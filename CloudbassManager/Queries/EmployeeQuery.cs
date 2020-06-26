using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class EmployeeQuery
    {
        /// <summary>
        /// Gets the currently logged in user.
        /// </summary>
        //[UseFirstOrDefault]
        //[UseSelection]
        //public IQueryable<Employee> GetEmployee(string currentUserEmail, [Service] CloudbassContext db)
        //{
        //    return db.Employees.Where(x => x.Email == currentUserEmail);
        //}


        /// <summary>
        /// Gets access to all the people known to this service.
        /// </summary>

        //[UsePaging]
        //[UseSelection]
        //[UseFiltering]
        //[UseSorting]
        //public IQueryable<Employee> GetEmployees([Service] CloudbassContext db)
        //{
        //    return db.Employees;
        //}

        /// <summary>
        /// Gets a user by its email address.
        /// </summary>
        //[UseFirstOrDefault]
        //[UseSelection]
        //public IQueryable<Employee> GetEmployeeByEmail([Service] CloudbassContext db, string email)
        //{
        //    return db.Employees.Where(x => x.Email == email);
        //}


        /// <summary>
        /// Gets a user by its id.
        /// </summary>
        //[UseFirstOrDefault]
        //[UseSelection]
        //public IQueryable<Employee> GetEmployeeById([Service] CloudbassContext db, int id)
        //{
        //    return db.Employees.Where(x => x.Id == id);
        //}
    }
}
