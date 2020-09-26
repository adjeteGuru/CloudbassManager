using Cloudbass.DataAccess.Repositories.Contracts;

using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Utilities.CustomException;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.DataAccess.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly CloudbassContext _db;
        public JobRepository(CloudbassContext db)
        {
            _db = db;
        }

        //To create
        public async Task<Job> CreateJobAsync(Job job, CancellationToken cancellationToken)
        {
            //    //Duplication job check
            var checkDuplication = await _db.Jobs.FirstOrDefaultAsync(x => x.Name == job.Name && x.StartDate == job.StartDate);

            if (checkDuplication != null)
            {
                throw new QueryException(
                   ErrorBuilder.New()
                       .SetMessage("Name " + job.Name + " is already scheduled on same Date " + job.StartDate)
                       .SetCode("NAME_EXIST")
                       .Build());
            }

            var addedJob = await _db.Jobs.AddAsync(job).ConfigureAwait(false);
            await _db.SaveChangesAsync();
            return addedJob.Entity;
        }

        public async Task<Job> GetJobByIdAsync(Guid jobId)
        {
            return await _db.Jobs.FindAsync(jobId);
        }


        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await _db.Jobs.AsNoTracking().ToListAsync();
        }

        // this GetJobsAsync method takes a list of job ids and returns a dictionary of jobs
        //with their ids as keys.
        //public async Task<IReadOnlyDictionary<Guid, Job>> GetJobsByIdAsync(
        //    IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        //{
        //    var list = await _db.Jobs.AsQueryable()
        //        .Where(x => ids.Contains(x.Id))
        //        .ToListAsync(cancellationToken)
        //        .ConfigureAwait(false);
        //    return list.ToDictionary(x => x.Id);
        //}

        public async Task<IReadOnlyDictionary<string, Job>> GetJobsByIdAsync(
      IReadOnlyList<string> ids, CancellationToken cancellationToken)
        {
            var list = await _db.Jobs.AsQueryable()
                .Where(x => ids.Contains(x.JobRef))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            return list.ToDictionary(x => x.JobRef);
        }

        public async Task<IEnumerable<Job>> GetJobByClientIdAsync(Guid clientId)
        {
            return await _db.Jobs.Where(x => x.ClientId == clientId)
                                 .ToListAsync();
        }

        //this method helps filter Jobs by client
        public async Task<ILookup<Guid, Job>> GetJobsByClientIdAsync(
            IReadOnlyList<Guid> clientIds, CancellationToken cancellationToken)
        {
            var filterJobs = await _db.Jobs
                 .Where(x => clientIds.Contains(x.ClientId))
                 .ToListAsync(cancellationToken);
            return filterJobs.ToLookup(x => x.ClientId);
        }

        public async Task<Job> UpdateJobAsync(Job job, CancellationToken cancellationToken)
        {
            var jobToUpdate = await _db.Jobs.FindAsync(job.Id);
            _db.Jobs.Update(jobToUpdate);
            await _db.SaveChangesAsync();
            return jobToUpdate;
        }

        public async Task<Job> DeleteJobAsync(Job job, CancellationToken cancellationToken)
        {
            //create a variable and check if job id match id field from the db
            var jobToDelete = await _db.Jobs.FirstOrDefaultAsync(x => x.Id == job.Id);

            if (jobToDelete == null)
            {
                throw new JobNotFoundException() { JobId = job.Id };
            }

            _db.Jobs.Remove(jobToDelete);

            await _db.SaveChangesAsync();

            return jobToDelete;
        }



        //public async Task<Job> CreateJobMembersAsync(Guid id, Guid hasRoleId, CancellationToken cancellationToken)
        //{
        //var jobToAllocate = await _db.Jobs.Where(x => x.Id == id).FirstOrDefaultAsync();
        //var crewMembers = _db.Jobs.

        //    throw new NotImplementedException();
        //}



        //public IEnumerable<Job> GetJobsForClient(int clientId, int lastJob)
        //{
        //    return _db.Jobs.Where(x => x.ClientId == clientId)
        //         //by decending order
        //         .OrderByDescending(x => x.CreatedAt)
        //         //take as many as client want
        //         .Take(lastJob);
        //}

        //public IQueryable<Job> GetAll()
        //{
        //    return _db.Jobs.AsQueryable();
        //}

        //public IEnumerable<Job> GetJobsForClient(int clientId)
        //{
        //    return _db.Jobs.Where(x => x.ClientId == clientId);
        //}
    }
}

