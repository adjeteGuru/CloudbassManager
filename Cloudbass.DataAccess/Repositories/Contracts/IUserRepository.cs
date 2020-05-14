

using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
using Cloudbass.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        User Authenticate(string name, string password);

        //this will return the collection of users as ienumerable
        IQueryable<User> GetAll();

        //this method return a single user by ID and take the "ID" as a parameter
        User GetById(int id);

        User Create(User user, string password);
        //void Delete(int id);
        User Delete(int id);
        //User Update(User user, string password = null);
        void Update(User user, string password = null);
    }
}
