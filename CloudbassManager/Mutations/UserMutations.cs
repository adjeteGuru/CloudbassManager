﻿using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.DataAccess.Repositories.Contracts.Inputs.User;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Types.Input;
using Cloudbass.Types.Payload;
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
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public UserMutations(IUserRepository userRepository, IEmployeeRepository employeeRepository)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
        }
        /// <summary>
        /// Creates a user.
        /// </summary>
        public async Task<CreateUserPayload> CreateUser(
            CreateUserInput input, [Service]IUserRepository userRepository,
            [Service] CloudbassContext db, [Service]
            IEmployeeRepository employeeRepository,
            [Service]ITopicEventSender eventSender)
        {
            //create a variable for dupication name check
            var nameCheck = await db.Users.FirstOrDefaultAsync(t => t.Name == input.Name);


            if (string.IsNullOrWhiteSpace(input.Name))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The name cannot be empty.")
                        .SetCode("NAME_EMPTY")
                        .Build());
            }

            //check dupication of the new entry
            if (nameCheck != null)
            {
                // throw error if the new username is already taken
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name \"" + input.Name + "\" is already taken")
                        .SetCode("NAME_EXIST")
                        .Build());
            }



            if (string.IsNullOrWhiteSpace(input.Password))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The password cannot be empty.")
                        .SetCode("PASSWORD_EMPTY")
                        .Build());
            }

            string salt = Guid.NewGuid().ToString("N");

            using var sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + salt));
            //Guid employeeId = Guid.NewGuid();


            var employee = new Employee
            {
                //Id = user.Id,
                // UserId = user.Id,
                // Id = employeeId,
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
                // Id = Guid.NewGuid(),
                EmployeeId = employee.Id,
                Name = input.Name,
                Email = input.Email,
                Password = Convert.ToBase64String(hash),
                Salt = salt
            };



            //create a variable for exiting email check
            var emailCheck = await db.Users.FirstOrDefaultAsync(x => x.Email == input.Email);

            //check dupication of the new entry
            if (emailCheck != null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage(input.Email + " is already been taken! Please chose different email.")
                        .SetCode("EMAIL_EXIST")
                        .Build());
            }

            if (!string.IsNullOrWhiteSpace(input.Email))
            {
                user.Email = input.Email;
            }

            if (input.Active.HasValue)
            {
                user.Active = input.Active.Value ? true : false;
            }

            //await userRepository.AddUserAsync(user).ConfigureAwait(false);

            //await employeeRepository.AddEmployeeAsync(employee).ConfigureAwait(false);

            db.Users.Add(user);
            db.Employees.Add(employee);

            await db.SaveChangesAsync();

            await eventSender.SendAsync("CreateUser", user/*,employee*/);

            return new CreateUserPayload(user/*, employee*/);
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        public async Task<UpdateUserPayload> UpdateUser(
            int id,
            UpdateUserInput input,
            [Service] CloudbassContext db)
        {
            var user = await db.Users.FindAsync(id);

            if (user == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("User not found in database.")
                        .SetCode("USER_NOT_FOUND")
                        .Build());
            }

            // update name if it has changed
            if (!string.IsNullOrWhiteSpace(input.Name) && input.Name != user.Name)
            {
                // throw error if the new name is already taken
                if (db.Users.Any(x => x.Name == input.Name))

                    throw new QueryException(
                        ErrorBuilder.New()
                            .SetMessage("Name " + input.Name + " is already taken")
                            .SetCode("NAME_EXIST")
                            .Build());

                user.Name = input.Name;
            }

            // update password if provided
            if (!string.IsNullOrWhiteSpace(input.Password))
            {

                using var sha = SHA512.Create();
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));
                user.Password = Convert.ToBase64String(hash);
            }


            if (!string.IsNullOrWhiteSpace(input.Email))
            {
                user.Email = input.Email;

                //create a variable for exiting email check
                var updatedEmailCheck = await db.Users.FirstOrDefaultAsync(x => x.Email == input.Email);

                //check dupication of the new entry
                if (updatedEmailCheck != null)
                {
                    throw new QueryException(
                        ErrorBuilder.New()
                            .SetMessage("Email " + input.Email + " is already taken")
                            .SetCode("EMAIL_EXIST")
                            .Build());
                }
            }

            if (input.Active.HasValue)
            {
                user.Active = input.Active.Value ? true : false;
            }


            db.Users.Update(user);

            await db.SaveChangesAsync();

            // 
            Serilog.Log
                .ForContext("MutationName", "UpdateUser")
                .ForContext("MutatedId", id)
                .ForContext("UserId", user.Name)
                .Information("{input}",
                             JsonConvert.SerializeObject(input));

            return new UpdateUserPayload(user);
        }


        //remove user
        public User DeleteUser(DeleteUserInput input)
        {
            return _userRepository.Delete(input);
        }

    }
}
