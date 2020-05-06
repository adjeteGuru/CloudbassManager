using Cloudbass.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cloudbass.Database
{
    public static class CloudbassSeedData
    {
        public static void EnsureSeedData(this CloudbassContext db)
        {
            if (!db.Users.Any() || !db.Clients.Any() || !db.Jobs.Any() || !db.Schedules.Any())
            {
                string salt = Guid.NewGuid().ToString("N");

                using var sha = SHA512.Create();
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes("Cloudba55" + salt));

                var users = new List<User>
                {
                    new User

                    {   Name = "Admin",
                        Password= Convert.ToBase64String(hash),
                        Salt = salt
                    }
                };

                db.Users.AddRange(users);
                db.SaveChanges();


                var clients = new List<Client>
                {
                    new Client
                    {
                        Name = "ITV",
                        ToContact = "Alexander Cooper",
                        Email = "alexander@gmail.com",
                        Address = "PO Box 12 london",
                        Tel = "02051122345"
                    },
                    new Client
                    {
                        Name = "BBC",
                        ToContact = "Dave Cooper",
                        Email = "dave@gmail.com",
                        Address = "PO Box 12 nottingham",
                        Tel = "01151122345" ,

                                Jobs= new List<Job>
                                {
                                    new Job
                                    {
                                         Text = "SPL",
                                        Description = "friendly",
                                        Location = "Scotland celtic park",
                                        Coordinator = "Dixon",
                                        CreatedAt = DateTime.Parse("2020-04-18"),
                                        StartDate = DateTime.Parse("2020-05-10"),
                                        EndDate = DateTime.Parse("2020-05-13"),

                                        CommercialLead = "Francis Akai",

                                    },

                                    new Job
                                    {
                                         Text = "MUTV",
                                        Description = "Accademy football",
                                        Location = "Old traford",
                                        Coordinator = "James",
                                        CreatedAt = DateTime.Parse("2020-04-18"),
                                        StartDate = DateTime.Parse("2020-06-10"),
                                        EndDate = DateTime.Parse("2020-06-12"),

                                        CommercialLead = "Luke Davies",

                                                    Schedules= new List<Schedule>
                                                    {

                                                         new Schedule
                                                         {
                                                             Name="SPL-Travel",
                                                             Description="first phase of the setting",
                                                             StartDate=DateTime.Parse("2020-05-11"),
                                                             EndDate=DateTime.Parse("2020-05-13"),
                                                             Status = Status.Active
                                                         }

                                                    }


                                    }
                                }
                    }
                };

                db.Clients.AddRange(clients);
                db.SaveChanges();

            }

        }
    }
}
