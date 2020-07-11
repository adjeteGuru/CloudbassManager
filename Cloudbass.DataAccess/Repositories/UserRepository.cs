using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.DataAccess.Repositories.Contracts.Inputs.User;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Utilities;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        // this property is to allow us to fetch data from the context
        //which is a standard injection into constructor
        private readonly CloudbassContext _db;
        private readonly AppSettings _appSettings;

        //constructor to allow dependency injection of context & appSettings
        public UserRepository(IOptions<AppSettings> appSettings, CloudbassContext db)

        {
            _db = db;
            _appSettings = appSettings.Value;

        }

        //implement function Authenticate set in the base class user interface
        public User Authenticate(string name, string password)
        {
            //create a user instance and perform a quick search from db to match up exisiting name & pswd
            var user = _db.Users.SingleOrDefault(x => x.Name == name && x.Password == password);

            // return null if user not found

            if (user == null)

                return null;

            // then create new user authentication will be successful so generate jwt token

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //token was created as string..so I applied the int convert.

            user.TokenVersion = Convert.ToInt32(tokenHandler.WriteToken(token));

            return user.WithoutPassword();

        }

        //this implement the interface member Repository User GetAll()
        public IEnumerable<User> GetAll()
        {
            //list Users;
            return _db.Users.WithoutPasswords();
        }

        //this method use linq "SingleOrDefault" statement with lambda function to compile the correct id
        //which is going to be use in the UserQuery 
        public User GetById(Guid id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }


        public User Delete(DeleteUserInput input)
        {

            var userToDelete = _db.Users.Find(input);

            if (userToDelete == null)
            {
                throw new QueryException(
                   ErrorBuilder.New()
                       .SetMessage("User not found in database.")
                       .SetCode("USER_NOT_FOUND")
                       .Build());
            }
            _db.Users.Remove(userToDelete);
            _db.SaveChanges();
            return userToDelete;
        }

        public async Task<User> GetUserAsync(string email, CancellationToken cancellationToken)
        {
            return await _db.Users.AsQueryable()
                .FirstOrDefaultAsync(x => x.Email == email)
                // this tells the Task that it can resume itself on any thread
                //that is available instead of waiting for the thread that originally created it.
                //This will speed up responses and avoid many deadlocks
                .ConfigureAwait(false);
        }



        //public async Task<User> UpdatePasswordAsync(
        //    string email, string newPAsswordHash, string salt, CancellationToken cancellationToken)
        //{
        //    var userToUpdate = await _db.Users.FindAsync(email);
        //    var updatedUser = _db.Users.Update(userToUpdate);
        //    await _db.SaveChangesAsync();
        //    return updatedUser.Entity;
        //}

        public async Task<User> UpdatePasswordAsync(
       string email, string newPAsswordHash, string salt, CancellationToken cancellationToken)
        {
            var userToUpdate = await _db.Users
                .Where(x => x.Email == email).FirstOrDefaultAsync();
            //.ConfigureAwait(false);
            var updatedUser = _db.Users.Update(userToUpdate);
            await _db.SaveChangesAsync()
            .ConfigureAwait(false);
            return updatedUser.Entity;
        }




        //public async Task<User> UpdatePasswordAsync(string email, string newPAsswordHash, string salt, CancellationToken cancellationToken)
        //{
        //    var userToUpdate = await _db.Users.AsQueryable()
        //    .Where(x => x.Email == email || x.Password == newPAsswordHash || x.Salt == salt)
        //    .FirstOrDefaultAsync(cancellationToken);
        //    //.SingleOrDefault(x => x.Email == email || x.Password == newPAsswordHash || x.Salt == salt)

        //    //var userToUpdate = await _db.Users.Where(x => x.Email == email || x.Password == newPAsswordHash || x.Salt == salt);
        //    //.FirstOrDefaultAsync(cancellationToken);
        //    var updatedUser = _db.Users.Update(userToUpdate.Password == newPAsswordHash, userToUpdate.Email==email);
        //    // var updatedUser = _db.Users.Where(x => x.Password == newPAsswordHash && x.Salt == salt);


        //    await _db.SaveChangesAsync()

        //        .ConfigureAwait(false);

        //    return updatedUser.Entity;


        //}

        public async Task<User> CreateUserAsync(User user)
        {
            var addedUser = await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync()
            .ConfigureAwait(false);
            return addedUser.Entity;
        }


    }
}
