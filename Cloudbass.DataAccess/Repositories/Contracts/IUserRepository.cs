

using Cloudbass.DataAccess.Repositories.Contracts.Inputs.User;
using Cloudbass.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        User Authenticate(string name, string password);

        //this will return the collection of users as ienumerable
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserAsync(string email, CancellationToken cancellationToken = default);

        Task<User> CreateUserAsync(User user);

        //Task UpdatePasswordAsync(string email, string newPAsswordHash, string salt, CancellationToken cancellationToken = default);

        //User Delete(DeleteUserInput input);

    }
}
