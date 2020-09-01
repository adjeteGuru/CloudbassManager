﻿using Cloudbass.DataAccess.Repositories.Contracts;
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
            //create a variable for dupication name check
            //var nameCheck = await db.Users.FirstOrDefaultAsync(t => t.Name == input.Name);

            //if (string.IsNullOrWhiteSpace(input.Name))
            //{
            //    throw new QueryException(
            //        ErrorBuilder.New()
            //            .SetMessage("The name cannot be empty.")
            //            .SetCode("NAME_EMPTY")
            //            .Build());
            //}

            //check dupication of the new entry
            //if (nameCheck != null)
            //{
            //    // throw error if the new username is already taken
            //    throw new QueryException(
            //        ErrorBuilder.New()
            //            .SetMessage("Name \"" + input.Name + "\" is already taken")
            //            .SetCode("NAME_EXIST")
            //            .Build());
            //}



            //if (string.IsNullOrWhiteSpace(input.Password))
            //{
            //    throw new QueryException(
            //        ErrorBuilder.New()
            //            .SetMessage("The password cannot be empty.")
            //            .SetCode("PASSWORD_EMPTY")
            //            .Build());
            //}

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
                Name = input.Name,
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

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                userToUpdate.Name = input.Name;
            }

            if (!string.IsNullOrWhiteSpace(input.Email))
            {
                userToUpdate.Email = input.Email;
            }

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

            await eventSender.SendAsync(userToUpdate, cancellationToken).ConfigureAwait(false);

            Serilog.Log
                .ForContext("MutationName", "UpdateUser")
                .ForContext("MutatedId", id)
                .ForContext("UserId", userToUpdate.Name)
                .Information("{input}",
                             JsonConvert.SerializeObject(input));


            return new UpdateUserPayload(userToUpdate);

            //// update name if it has changed
            //if (!string.IsNullOrWhiteSpace(input.Name) && input.Name != user.Name)
            //{
            //    // throw error if the new name is already taken
            //    if (db.Users.Any(x => x.Name == input.Name))

            //        throw new QueryException(
            //            ErrorBuilder.New()
            //                .SetMessage("Name " + input.Name + " is already taken")
            //                .SetCode("NAME_EXIST")
            //                .Build());

            //    user.Name = input.Name;
            //}

            // update password if provided
            //if (!string.IsNullOrWhiteSpace(input.Password))
            //{

            //    using var sha = SHA512.Create();
            //    byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));
            //    user.Password = Convert.ToBase64String(hash);
            //}


            //if (!string.IsNullOrWhiteSpace(input.Email))
            //{
            //    user.Email = input.Email;

            //    //create a variable for exiting email check
            //    var updatedEmailCheck = await db.Users.FirstOrDefaultAsync(x => x.Email == input.Email);

            //    //check dupication of the new entry
            //    if (updatedEmailCheck != null)
            //    {
            //        throw new QueryException(
            //            ErrorBuilder.New()
            //                .SetMessage("Email " + input.Email + " is already taken")
            //                .SetCode("EMAIL_EXIST")
            //                .Build());
            //    }
            //}

            //if (input.Active.HasValue)
            //{
            //    user.Active = input.Active.Value ? true : false;
            //}

            //if (input.IsAdmin.HasValue)
            //{
            //    user.IsAdmin = input.IsAdmin.Value ? true : false;
            //}


            //db.Users.Update(user);

            //await db.SaveChangesAsync();

            // 
            //Serilog.Log
            //    .ForContext("MutationName", "UpdateUser")
            //    .ForContext("MutatedId", id)
            //    .ForContext("UserId", user.Name)
            //    .Information("{input}",
            //                 JsonConvert.SerializeObject(input));

            //return new UpdateUserPayload(user);


        }


    }
}
