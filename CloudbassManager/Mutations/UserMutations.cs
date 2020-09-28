using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Types.Users;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {

        /// <summary>
        /// Creates a user.
        /// </summary>
        public async Task<CreateUserPayload> CreateUserAsync(
            CreateUserInput input,
            [Service]IUserRepository userRepository,
            [Service]IEmployeeRepository employeeRepository,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {

            string salt = Guid.NewGuid().ToString("N");

            using var sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + salt));

            Guid employeeId = Guid.NewGuid();


            var employee = new Employee
            {
                Id = employeeId,
                CountyId = input.CountyId,
                PostNominals = input.PostNominals,
                Alergy = input.Alergy,
                NextOfKin = input.NextOfKin,
                Bared = input.Bared,
                Email = input.Email,
                FullName = input.FullName,
                Photo = input.Photo

            };


            var user = new User
            {
                Id = Guid.NewGuid(),
                EmployeeId = employeeId,
                //Name = input.Name,
                PermisionLevel = input.PermisionLevel,
                Email = input.Email,
                Password = Convert.ToBase64String(hash),
                Salt = salt
            };


            await employeeRepository.CreateEmployeeAsync(employee, cancellationToken).ConfigureAwait(false);

            await userRepository.CreateUserAsync(user).ConfigureAwait(false);

            await eventSender.SendAsync("CreateUser", user);

            return new CreateUserPayload(user);
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        public async Task<UpdateUserPayload> UpdateUserAsync(
            UpdateUserInput input, Guid id,
            [Service]IUserRepository userRepository,
            [Service]IEmployeeRepository employeeRepository,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var userToUpdate = await userRepository.GetUserByIdAsync(id);

            if (userToUpdate == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("User not found in database.")
                        .SetCode("USER_NOT_FOUND")
                        .Build());
            }

            if (!string.IsNullOrWhiteSpace(input.PermisionLevel))
            {
                userToUpdate.PermisionLevel = input.PermisionLevel;
            }

            // update emai if it has changed
            if (!string.IsNullOrWhiteSpace(input.Email) && input.Email == userToUpdate.Email)
            {
                // throw error            

                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Email " + input.Email + " is already taken")
                        .SetCode("EMAIL_EXIST")
                        .Build());
            }

            userToUpdate.Email = input.Email;


            if (!string.IsNullOrWhiteSpace(input.Password))
            {
                using var sha = SHA512.Create();
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + userToUpdate.Salt));
                userToUpdate.Password = Convert.ToBase64String(hash);
            }


            if (input.Active.HasValue)
            {
                userToUpdate.Active = input.Active.Value ? true : false;
            }


            await userRepository.UpdateUserAsync(userToUpdate, cancellationToken).ConfigureAwait(false);

            //await employeeRepository.UpdateEmployeeAsync(userToUpdate, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(userToUpdate, cancellationToken).ConfigureAwait(false);

            Serilog.Log
                .ForContext("MutationName", "UpdateUser")
                .ForContext("MutatedId", id)
                .ForContext("UserId", userToUpdate.Email)
                .Information("{input}",
                             JsonConvert.SerializeObject(input));


            return new UpdateUserPayload(userToUpdate);

        }


        //delete
        public async Task<User> DeleteUserAsync(
           [Service] IUserRepository userRepository,
           [Service] ITopicEventSender eventSender,
           DeleteUserInput input, CancellationToken cancellationToken)
        {
            var userToDelete = await userRepository.GetUserByIdAsync(input.Id);

            await userRepository.DeleteUserAsync(userToDelete, cancellationToken).ConfigureAwait(false);

            await eventSender.SendAsync(userToDelete, cancellationToken).ConfigureAwait(false);

            return userToDelete;

        }


    }
}
