using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Utilities;
using Cloudbass.Utilities.CustomException;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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


        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public User GetById(int id)
        {
            return _db.Users.Find(id);
        }


        //    //create a user instance and perform a quick search from db to match up exisiting name & pswd
        //    var user = _db.Users.SingleOrDefault(x => x.Name == name && x.Password == password);
        //    //var user = _db.Users.SingleOrDefault(x => x.Name == name);


        public User Authenticate(string name, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                return null;

            var user = _db.Users.SingleOrDefault(x => x.Name == name);

            // check if Name exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


        //create
        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_db.Users.Any(x => x.Name == user.Name))
                throw new AppException("Name \"" + user.Name + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _db.Users.Add(user);
            _db.SaveChanges();

            return user;
        }


        // private helper methods
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new AppException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //update
        public void Update(User userParam, string password = null)
        {
            var user = _db.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update Name if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Name) && userParam.Name != user.Name)
            {
                // throw error if the new Name is already taken
                if (_db.Users.Any(x => x.Name == userParam.Name))
                    throw new AppException("Name " + userParam.Name + " is already taken");

                user.Name = userParam.Name;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FullName))
                user.FullName = userParam.FullName;

            if (!string.IsNullOrWhiteSpace(userParam.Email))
                user.Email = userParam.Email;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _db.Users.Update(user);
            _db.SaveChanges();
        }

        //delete
        public User Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }

            return user;
        }


    }
}
