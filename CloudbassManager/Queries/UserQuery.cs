
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Linq;


namespace CloudbassManager.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class UserQuery
    {
        private readonly IUserRepository _userRepository;
        public UserQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Gets all users.
        /// </summary>
        //[Authorize]
        //public IQueryable<User> GetUsers([Service] CloudbassContext db) =>

        //    db.Users;

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }
        /// <summary>
        /// Gets a user by its id.
        /// </summary>
       // [Authorize]
        //public IQueryable<User> GetUser([Service] CloudbassContext db, int id) =>

        //    db.Users.Where(t => t.Id == id);

        //public IQueryable<User> GetUser(int id)
        //{
        //    _userRepository.Where(x=>x.);
        //}

    }
}
