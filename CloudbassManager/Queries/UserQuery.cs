using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using GraphQL.Types;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class UserQuery
    {

        //public UserQuery()
        //{

        //}

        ///// <summary>
        ///// Gets all users.
        ///// </summary>
        //public async Task<IReadOnlyList<User>> GetUsers([Service] CloudbassContext dbContext)
        //{
        //    return await dbContext.Users.ToListAsync();

        //}

        ///// <summary>
        ///// Gets a user by its id.
        ///// </summary>
        //public async Task<User> GetUser([Service] CloudbassContext dbContext, int id)
        //{
        //    return await dbContext.Users.FindAsync(id);
        //}

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
        public IQueryable<User> GetUser([Service] CloudbassContext db, int id) =>

            db.Users.Where(t => t.Id == id);


    }
}
