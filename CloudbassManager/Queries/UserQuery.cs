
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using System;
using System.Linq;


namespace CloudbassManager.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class UserQuery
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        [Authorize]
        public IQueryable<User> GetUsers([Service] CloudbassContext db) =>

            db.Users;

        /// <summary>
        /// Gets a user by its id.
        /// </summary>
        [Authorize]
        public IQueryable<User> GetUser([Service] CloudbassContext db, Guid id) =>

            db.Users.Where(t => t.Id == id);

    }
}
