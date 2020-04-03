using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        // this property is to allow us to fetch data from the context
        //which is a standard injection into constructor
        private readonly CloudbassContext _dbContext;
        private readonly AppSettings _appSettings;
        public UserRepository(IOptions<AppSettings> appSettings, CloudbassContext dbContext)

        {
            _dbContext = dbContext;
            _appSettings = appSettings.Value;

        }

        public User Authenticate(string name, string password)
        {

            var user = _dbContext.Users.SingleOrDefault(x => x.Name == name && x.Password == password);

            // return null if user not found

            if (user == null)

                return null;
            // authentication successful so generate jwt token

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

        //this implement the interface member Repository.GetAll()
        public IEnumerable<User> GetAll()
        {
            // return _dbContext.Users;
            return _dbContext.Users.WithoutPasswords();
        }

        //this method use linq "SingleOrDefault" statement with lambda function to compile the correct id
        //which is going to be use in the UserQuery 
        public User GetById(int id)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}
