using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Utilities.CustomException;
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

        public Job Create(CreateJobInput input)
        {
            var job = new Job
            {
                Text = input.Text,

                Description = input.Description,
                Location = input.Location,
                CreatedAt = input.CreatedAt,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                TXDate = input.TXDate,
                Paid = input.Paid,
                Coordinator = input.Coordinator,
                CommercialLead = input.CommercialLead,
                ClientId = input.ClientId,
                Status = input.Status

            };

            _db.Jobs.Add(job);
            //_db.SaveChanges();
            return job; /*new Job();*/

        }


        public Job Delete(DeleteJobInput input)
        {//create a variable and check if input id match id field from the db
            var jobToDelete = _db.Jobs.FirstOrDefault(x => x.Id == input.Id);

            if (jobToDelete == null)
                //{
                throw new JobNotFoundException() { JobId = input.Id };
            //}


            _db.Jobs.Remove(jobToDelete);
            return jobToDelete;
        }

        //public Job DeleteJob(DeleteJobInput inputJob)
        //{
        //    return _db.Jobs.Remove(inputJob);

        //}

        //public IEnumerable<Job> GetJobs()
        //{
        //    return _db.Jobs;
        //}

        IQueryable<Job> IJobRepository.GetAll()
        {
            return _db.Jobs.AsQueryable();
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

