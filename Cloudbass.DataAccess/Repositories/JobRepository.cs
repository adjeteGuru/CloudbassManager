using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.DataAccess.Repositories.Contracts.Inputs;
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
        public async Task<Job> CreateJobAsync(Job job/*, CancellationToken cancellationToken*/)
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

            var addedJob = await _db.Jobs.AddAsync(job);
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
        public async Task<IReadOnlyDictionary<Guid, Job>> GetJobsByIdAsync(
            IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var list = await _db.Jobs.AsQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            return list.ToDictionary(x => x.Id);
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

        public Task<IEnumerable<Job>> GetJobsByEmployeeIdAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        //public async Task<ILookup<Guid, Job>> GetJobsByEmployeeIdAsync(
        //    IReadOnlyList<Guid> employeeIds, CancellationToken cancellationToken)
        //{
        //    var list = await _db.Jobs
        //        .Where(x => employeeIds.Contains(x.i))
        //        .ToListAsync(cancellationToken);
        //    return jobs.ToLookup(x => x.Name);
        //}



        //public async Task<ILookup<string, Job>> GetEmployeesByJobName(
        //   IReadOnlyList<string> onjobs, CancellationToken cancellationToken)
        //{
        //    var jobs = await _db.Jobs
        //        .Where(x => onjobs.Contains(x.Name))
        //        .ToListAsync(cancellationToken);
        //    return jobs.ToLookup(x => x.Name);
        //}

        //public Task UpdateJobAsync(Job job, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        ////this create an object and assigned inputs data to store into job db as new record
        //public Job Create(CreateJobInput input)
        //{
        //    //Duplication job check
        //    var checkDuplication = _db.Jobs.FirstOrDefault(x => x.StartDate == input.StartDate && x.Name == input.Name);

        //    if (checkDuplication != null)
        //    {
        //        throw new QueryException(
        //           ErrorBuilder.New()
        //               .SetMessage("Name " + input.Name + " is already scheduled on same Date " + input.StartDate)
        //               .SetCode("NAME_EXIST")
        //               .Build());
        //    }

        //    var job = new Job
        //    {
        //        Name = input.Name,

        //        Description = input.Description,
        //        Location = input.Location,
        //        CreatedAt = input.CreatedAt,
        //        StartDate = input.StartDate,
        //        EndDate = input.EndDate,
        //        TXDate = input.TXDate,
        //        Paid = input.Paid,
        //        Coordinator = input.Coordinator,
        //        CommercialLead = input.CommercialLead,
        //        ClientId = input.ClientId,
        //        Status = input.Status

        //    };

        //    _db.Jobs.Add(job);

        //    _db.SaveChanges();

        //    return job;
        //}


        //public Job Delete(DeleteJobInput input)
        //{
        //    //create a variable and check if input id match id field from the db
        //    var jobToDelete = _db.Jobs.FirstOrDefault(x => x.Id == input.Id);

        //    if (jobToDelete == null)
        //    {
        //        throw new JobNotFoundException() { JobId = input.Id };
        //    }

        //    _db.Jobs.Remove(jobToDelete);

        //    _db.SaveChanges();

        //    return jobToDelete;
        //}

        //public Job Update(UpdateJobInput input, Guid id)
        //{
        //    //quick search for input id from identical db id 
        //    var jobToUpdate = _db.Jobs.Find(id);

        //    if (jobToUpdate == null)
        //    {
        //        throw new JobNotFoundException() { JobId = input.Id };
        //    }

        //    if (!string.IsNullOrWhiteSpace(input.Name))
        //    {
        //        jobToUpdate.Name = input.Name;
        //    }

        //    if (!string.IsNullOrWhiteSpace(input.Description))
        //    {
        //        jobToUpdate.Description = input.Description;
        //    }

        //    if (!string.IsNullOrWhiteSpace(input.Coordinator))
        //    {
        //        jobToUpdate.Coordinator = input.Coordinator;
        //    }

        //    if (!string.IsNullOrWhiteSpace(input.CommercialLead))
        //    {
        //        jobToUpdate.CommercialLead = input.CommercialLead;
        //    }

        //    if (!string.IsNullOrWhiteSpace(input.Status.ToString()))
        //    {
        //        jobToUpdate.Status = input.Status;
        //    }

        //    if (input.StartDate != null)
        //    {
        //        jobToUpdate.StartDate = input.StartDate;
        //    }

        //    if (input.TXDate != null)
        //    {
        //        jobToUpdate.TXDate = input.TXDate;
        //    }

        //    if (input.EndDate != null)
        //    {
        //        jobToUpdate.EndDate = input.EndDate;
        //    }

        //    _db.Jobs.Update(jobToUpdate);

        //    _db.SaveChanges();

        //    return jobToUpdate;
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

