﻿using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Database.Models;
using Cloudbass.Utilities.CustomException;
using HotChocolate;
using HotChocolate.Execution;
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

        //this create an object and assigned inputs data to store into job db as new record
        public Job Create(CreateJobInput input)
        {
            //Duplication job check
            var checkDuplication = _db.Jobs.FirstOrDefault(x => x.StartDate == input.StartDate && x.Text == input.Text);

            if (checkDuplication != null)
            {
                throw new QueryException(
                   ErrorBuilder.New()
                       .SetMessage("There is existing Job Name found in the database scheduled on same Date " + input.StartDate)
                       .SetCode("NAME_EXIST")
                       .Build());
            }

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
            _db.SaveChanges();
            return job;

        }


        //public Job Delete(DeleteJobInput input)
        //{
        //    //create a variable and check if input id match id field from the db
        //    var jobToDelete = _db.Jobs.FirstOrDefault(x => x.Id == input.Id);

        //    if (jobToDelete == null)
        //        //{
        //        throw new JobNotFoundException() { JobId = input.Id };
        //    //}


        //    _db.Jobs.Remove(jobToDelete);
        //    return jobToDelete;
        //}

        public Job Delete(DeleteJobInput input)
        {
            //create a variable and check if input id match id field from the db
            var jobToDelete = _db.Jobs.FirstOrDefault(x => x.Id == input.Id);

            if (jobToDelete == null)
            {
                throw new JobNotFoundException() { JobId = input.Id };
            }


            _db.Jobs.Remove(jobToDelete);
            _db.SaveChanges();
            return jobToDelete;
        }


        IQueryable<Job> IJobRepository.GetAll()
        {
            return _db.Jobs.AsQueryable();
        }


        //public IEnumerable<Job> GetJobsForClient(int clientId, int lastJob)
        //{
        //    return _db.Jobs.Where(x => x.ClientId == clientId)
        //         //by decending order
        //         .OrderByDescending(x => x.CreatedAt)
        //         //take as many as client want
        //         .Take(lastJob);
        //}


    }
}

