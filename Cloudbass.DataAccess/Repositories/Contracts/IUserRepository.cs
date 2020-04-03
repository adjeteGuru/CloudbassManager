

using Cloudbass.Database.Models;
using System.Collections.Generic;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        //this will return the collection of client as ienumerable
        User Authenticate(string name, string password);
        IEnumerable<User> GetAll();

        //this method return a single user by ID and take the "ID" as a parameter
        User GetById(int id);
    }
}
