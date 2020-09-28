
using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        //User Authenticate(string email, string password);

        //this will return the collection of users as ienumerable
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(Guid userId);

        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<User> CreateUserAsync(User user);

        //Task UpdatePasswordAsync(string email, string newPAsswordHash, string salt, CancellationToken cancellationToken = default);

        Task<User> DeleteUserAsync(User user, CancellationToken cancellationToken);

        Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);

    }
}
