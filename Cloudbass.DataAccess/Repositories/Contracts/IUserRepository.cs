

using Cloudbass.DataAccess.Repositories.Contracts.Inputs.User;
using Cloudbass.Database.Models;
using System.Collections.Generic;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        User Authenticate(string name, string password);

        //this will return the collection of users as ienumerable
        IEnumerable<User> GetAll();

        //this method return a single user by ID and take the "ID" as a parameter
        User GetById(int id);

        User Delete(DeleteUserInput input);
    }
}
