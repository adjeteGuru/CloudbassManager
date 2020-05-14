using AutoMapper;
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
//using Cloudbass.Types.Input;
//using Cloudbass.Types.Payload;
using Cloudbass.Utilities;
using Cloudbass.Utilities.CustomException;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
    public class UserMutations
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public UserMutations(IUserRepository userRepository, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }


        public User Login(AuthenticateModel model)
        {
            var user = _userRepository.Authenticate(model.Name, model.Password);

            //user or password check
            if (user == null)
            {
                throw new ArgumentException("Name or password is incorrect");

            }

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
            var tokenString = tokenHandler.WriteToken(token);


            // return basic user info and authentication token
            return user;


            //return new User
            //{
            //    Id = user.Id,
            //    Name = user.Name,
            //    FullName = user.FullName,
            //    Email = user.Email,

            //    Token = tokenString
            //}; 
        }

        public User Update(int id, UpdateModel model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = id;

            try
            {
                // update user 
                _userRepository.Update(user, model.Password);
                return user;
            }
            catch
            {
                // return error message if there was an exception
                //return BadRequest(new { message = ex.Message });
                throw new ArgumentException("Name or password is incorrect");
            }
        }

        public User Register(RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _userRepository.Create(user, model.Password);
                return user;
            }
            catch
            {
                // return error message if there was an exception
                throw new ArgumentException("Name or password is incorrect");
            }
        }

        //public async Task<CreateUserPayload> CreateUser(
        //    CreateUserInput input,
        //    [Service] CloudbassContext db,
        //    [Service]ITopicEventSender eventSender)
        //{
        //    //create a variable for dupication name check
        //    var nameCheck = await db.Users.FirstOrDefaultAsync(t => t.Name == input.Name);


        //    if (string.IsNullOrEmpty(input.Name))
        //    {
        //        throw new QueryException(
        //            ErrorBuilder.New()
        //                .SetMessage("The name cannot be empty.")
        //                .SetCode("NAME_EMPTY")
        //                .Build());
        //    }

        //    //check dupication of the new entry
        //    if (nameCheck != null)
        //    {

        //        throw new QueryException(
        //            ErrorBuilder.New()
        //                .SetMessage(input.Name + " Exist already in the database! Please chose different name.")
        //                .SetCode("NAME_EXIST")
        //                .Build());
        //    }



        //    if (string.IsNullOrEmpty(input.Password))
        //    {
        //        throw new QueryException(
        //            ErrorBuilder.New()
        //                .SetMessage("The password cannot be empty.")
        //                .SetCode("PASSWORD_EMPTY")
        //                .Build());
        //    }

        //    string salt = Guid.NewGuid().ToString("N");

        //    using var sha = SHA512.Create();
        //    byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + salt));


        //    var user = new User()
        //    {
        //        Name = input.Name,
        //        FullName = input.FullName,
        //        Password = Convert.ToBase64String(hash),
        //        Salt = salt
        //    };

        //    //create a variable for exiting email check
        //    var emailCheck = await db.Users.FirstOrDefaultAsync(x => x.Email == input.Email);

        //    //check dupication of the new entry
        //    if (emailCheck != null)
        //    {
        //        throw new QueryException(
        //            ErrorBuilder.New()
        //                .SetMessage(input.Email + " is already been taken! Please chose different email.")
        //                .SetCode("EMAIL_EXIST")
        //                .Build());
        //    }

        //    if (!string.IsNullOrEmpty(input.Email))
        //    {
        //        user.Email = input.Email;
        //    }

        //    if (!string.IsNullOrEmpty(input.FullName))
        //    {
        //        user.FullName = input.FullName;
        //    }

        //    if (input.Active.HasValue)
        //    {
        //        user.Active = input.Active.Value ? true : false;
        //    }


        //    db.Users.Add(user);

        //    await db.SaveChangesAsync();

        //    await eventSender.SendAsync("CreateUser", user);

        //    return new CreateUserPayload(user);
        //}



        /// <summary>
        /// Updates a user.
        /// </summary>
        //public async Task<UpdateUserPayload> UpdateUser(
        //    int id,
        //    UpdateUserInput input,
        //    [Service] CloudbassContext db)
        //{
        //    var user = await db.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        throw new QueryException(
        //            ErrorBuilder.New()
        //                .SetMessage("User not found in database.")
        //                .SetCode("USER_NOT_FOUND")
        //                .Build());
        //    }


        //    if (!string.IsNullOrEmpty(input.Name))
        //    {
        //        user.Name = input.Name;
        //    }

        //    if (!string.IsNullOrEmpty(input.FullName))
        //    {
        //        user.FullName = input.FullName;
        //    }

        //    if (!string.IsNullOrEmpty(input.Password))
        //    {

        //        using var sha = SHA512.Create();
        //        byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));
        //        user.Password = Convert.ToBase64String(hash);
        //    }


        //    if (!string.IsNullOrEmpty(input.Email))
        //    {
        //        user.Email = input.Email;

        //        //create a variable for exiting email check
        //        var updatedEmailCheck = await db.Users.FirstOrDefaultAsync(x => x.Email == input.Email);

        //        //check dupication of the new entry
        //        if (updatedEmailCheck != null)
        //        {
        //            throw new QueryException(
        //                ErrorBuilder.New()
        //                    .SetMessage(input.Email + " is already been taken! Please chose different email.")
        //                    .SetCode("EMAIL_EXIST")
        //                    .Build());
        //        }
        //    }

        //    if (input.Active.HasValue)
        //    {
        //        user.Active = input.Active.Value ? true : false;
        //    }


        //    db.Users.Update(user);

        //    await db.SaveChangesAsync();

        //    // 
        //    Serilog.Log
        //        .ForContext("MutationName", "UpdateUser")
        //        .ForContext("MutatedId", id)
        //        .ForContext("UserId", user.Name)
        //        .Information("{input}",
        //                     JsonConvert.SerializeObject(input));

        //    return new UpdateUserPayload(user);
        //}


        public User Delete(int id)
        {
            return _userRepository.Delete(id);

        }

        //public User GetAll()
        //{
        //    var users = _userRepository.GetAll();
        //    var model = _mapper.Map<IList<UserModel>>(users);
        //    return users;
        //}

        public User GetById(int id)
        {
            var user = _userRepository.GetById(id);
            var model = _mapper.Map<UserModel>(user);
            return user;
        }
    }
}
