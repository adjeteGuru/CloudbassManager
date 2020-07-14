using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories.Contracts
{
    public interface ICrewRepository
    {
        Task<IEnumerable<Crew>> GetCrewAsync();
        //IQueryable<Crew> GetAllCrewsAsync();
        Task<Crew> CreateCrewAsync(Crew crew);
    }
}
