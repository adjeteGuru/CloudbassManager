using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Types.Employees;
using Cloudbass.Types.Input;
using Cloudbass.Types.Payload;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class LoginMutation
    {

        public async Task<LoginPayload> LoginAsync(
          LoginInput input,
          [Service] CloudbassContext db,
          [Service] EmployeeByEmailDataLoader employeeByEmail,
          [Service] ITopicEventSender eventSender,
          CancellationToken cancellationToken
            //[Service]IUserRepository userRepository,
            //[Service] IEmployeeRepository employeeRepository
            )
        {
            //// Initialise the contact that is going to be returned
            //User user = null;


            if (string.IsNullOrEmpty(input.Email))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Please enter an email.")
                        .SetCode("EMAIL_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Password))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Please enter a password.")
                        .SetCode("PASSWORD_EMPTY")
                        .Build());
            }

            // User? user = await userRepository.GetUserAsync(x => x.input.).ConfigureAwait(false);

            //create a variable for dupication name check
            var user = await db.Users.FirstOrDefaultAsync(t => t.Email == input.Email);



            if (user == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified username is invalid.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }

            using var sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));

            if (!Convert.ToBase64String(hash).Equals(user.Password, StringComparison.Ordinal))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Password is incorrect.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }


            Employee employee = await employeeByEmail.LoadAsync(input.Email, cancellationToken);

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),

               // new Claim(WellKnownClaimTypes.UserId, employee.Id.ToString()),

            });

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Startup.SharedSecret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string accessToken = tokenHandler.WriteToken(token);

            await eventSender.SendAsync<string, Employee>("online", employee);

            return new LoginPayload(user, accessToken);
        }
    }
}
