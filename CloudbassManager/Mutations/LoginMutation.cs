using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Types.Input;
using Cloudbass.Types.Payload;
using HotChocolate;
using HotChocolate.Execution;
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
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class LoginMutation
    {

        public async Task<LoginPayload> LoginAsync(
          LoginInput input,
          [Service] CloudbassContext db)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Please enter a name.")
                        .SetCode("NAME_EMPTY")
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

            User? user = await db.Users.FirstOrDefaultAsync(t => t.Name == input.Name);

            if (user is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name is invalid.")
                        .SetCode("NAME_INVALID")
                        .Build());
            }

            using var sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));

            if (!Convert.ToBase64String(hash).Equals(user.Password, StringComparison.Ordinal))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Password is incorrect.")
                        .SetCode("PASSWORD_INVALID")
                        .Build());
            }


            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name)
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


            return new LoginPayload(user, accessToken);
        }
    }
}
