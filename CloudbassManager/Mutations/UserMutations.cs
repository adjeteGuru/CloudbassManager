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
        /// <summary>
        /// Creates a user.
        /// </summary>
        public async Task<CreateUserPayload> CreateUser(
            CreateUserInput input,
            [Service] CloudbassContext db,
            [Service]ITopicEventSender eventSender)
        {
            //create a variable for dupication name check
            var nameCheck = await db.Users.FirstOrDefaultAsync(t => t.Name == input.Name);


            if (string.IsNullOrEmpty(input.Name))
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

                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage(input.Name + " Exist already in the database! Please chose different name.")
                        .SetCode("NAME_EXIST")
                        .Build());
            }



            if (string.IsNullOrEmpty(input.Password))
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


            var user = new User()
            {
                Name = input.Name,
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

            if (!string.IsNullOrEmpty(input.Email))
            {
                user.Email = input.Email;
            }

            if (input.Active.HasValue)
            {
                user.Active = input.Active.Value ? true : false;
            }


            db.Users.Add(user);

            await db.SaveChangesAsync();

            await eventSender.SendAsync("CreateUser", user);

            return new CreateUserPayload(user);
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


            if (!string.IsNullOrEmpty(input.Name))
            {
                user.Name = input.Name;
            }

            if (!string.IsNullOrEmpty(input.Password))
            {

                using var sha = SHA512.Create();
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));
                user.Password = Convert.ToBase64String(hash);
            }


            if (!string.IsNullOrEmpty(input.Email))
            {
                user.Email = input.Email;

                //create a variable for exiting email check
                var updatedEmailCheck = await db.Users.FirstOrDefaultAsync(x => x.Email == input.Email);

                //check dupication of the new entry
                if (updatedEmailCheck != null)
                {
                    throw new QueryException(
                        ErrorBuilder.New()
                            .SetMessage(input.Email + " is already been taken! Please chose different email.")
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
    }
}
