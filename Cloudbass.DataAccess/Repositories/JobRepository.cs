using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbass.DataAccess.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly CloudbassContext _db;
        public JobRepository(CloudbassContext db)
        {
            _db = db;
        }

        public Job Create(CreateJobInput inputJob/*, [Service] CloudbassContext db*/)
        {
            var job = new Job
            {
                Text = inputJob.Text,

                Description = inputJob.Description,
                Location = inputJob.Location,
                CreatedAt = inputJob.CreatedAt,
                StartDate = inputJob.StartDate,
                EndDate = inputJob.EndDate,
                TXDate = inputJob.TXDate,
                Paid = inputJob.Paid,
                Coordinator = inputJob.Coordinator,
                CommercialLead = inputJob.CommercialLead,
                ClientId = inputJob.ClientId,
                Status = inputJob.Status

            };

            _db.Jobs.Add(job);
            _db.SaveChanges();
            return new Job();

        }


        public Job Delete(DeleteJobInput inputJob)
        {
            throw new NotImplementedException();
        }

        //public Job DeleteJob(DeleteJobInput inputJob)
        //{
        //    return _db.Jobs.Remove(inputJob);

        //}

        public IEnumerable<Job> GetJobs()
        {
            return _db.Jobs;
        }

        //public IEnumerable<Job> GetJobsForClient(int clientId)
        //{
        //    return _db.Jobs.Where(x => x.ClientId == clientId);
        //}

        //public IEnumerable<Job> GetJobsForClient(int clientId, int lastJob)
        //{
        //    return _db.Jobs.Where(x => x.ClientId == clientId)
        //         //by decending order
        //         .OrderByDescending(x => x.CreatedAt)
        //         //take as many as client want
        //         .Take(lastJob);
        //}


        //
        //public IEnumerable<object> Search(string text)
        //{
        //    IEnumerable<Job> filteredJobs = _db.Jobs
        //       .Where(t => t.Text.Contains(text,
        //           StringComparison.OrdinalIgnoreCase));

        //    foreach (Job job in filteredJobs)
        //    {
        //        yield return job;
        //    }

        //}

    }
}

