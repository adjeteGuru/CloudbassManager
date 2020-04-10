using Cloudbass.Database;
using Cloudbass.Types.Input;
using Cloudbass.Types.Payload;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CloudbassManager.Mutations
{
    public class UserMutations
    {
        /// <summary>
        /// Creates a user.
        /// </summary>
        public async Task<CreateUserPayload> CreateUser(
            CreateUserInput input,
            [Service] CloudbassContext db,
            [Service]ITopicEventSender eventSender, string username)
        {
            // var nameCheck = db.Users.Where(x => x.Name == username).SingleOrDefault();


            if (string.IsNullOrEmpty(input.Name))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The name cannot be empty.")
                        .SetCode("NAME_EMPTY")
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

            //var user = new User
            //{
            //    Name = input.Name,
            //    Password = Convert.ToBase64String(hash),
            //    Salt = salt
            //};



            //create a variable to check if username exist in the db or is new entry
            //var user = db.Users.FirstOrDefaultAsync(x => x.Name == username);
            var user = db.Users.Where(x => x.Name == username).FirstOrDefault();
            if (!string.IsNullOrEmpty(input.Name))
            {
                user.Name = input.Name;
                user.Password = Convert.ToBase64String(hash);
                user.Salt = salt;
            }
            else
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name chosen is already Exist.")
                        .SetCode("NAME_EXIST")
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
            }

            if (input.Active.HasValue)
            {
                user.Active = input.Active.Value ? true : false;
            }


            db.Users.Update(user);

            await db.SaveChangesAsync();

            Serilog.Log
                .ForContext("MutationName", "updateUser")
                .ForContext("MutationId", id)
                .Information("{input}", JsonConvert.SerializeObject(input));

            return new UpdateUserPayload(user);
        }
    }
}
