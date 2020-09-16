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
            if (!db.Users.Any() || !db.Clients.Any() || !db.Jobs.Any() || !db.Schedules.Any() || !db.Employees.Any() || !db.Counties.Any() || !db.HasRoles.Any() || !db.Roles.Any())
            {
                string salt = Guid.NewGuid().ToString("N");

                using var sha = SHA512.Create();
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes("Cloudba55" + salt));

                Guid employeeId = Guid.NewGuid();
                //Guid countyId = Guid.NewGuid();


                var counties = new List<County>
                                {

                                    //new County
                                    //{
                                    //    Name="Nottinghamshire"
                                    //},


                                    new County
                                    {
                                        Name = "Derbyshire",

                                       Employees=new List<Employee>
                                       {

                                           //new Employee
                                           //{
                                           //     FullName = "Mike Bob",
                                           //     Email = "mike.bob@gmail.com",
                                           //      Role = "Rigger",

                                           //     Users= new List<User>
                                           //     {
                                           //        new User
                                           //        {
                                           //            Name = "SuperAdmin",
                                           //            Password= Convert.ToBase64String(hash),
                                           //            Salt = salt,
                                           //        }
                                           //     }

                                           //},


                                           new Employee
                                           {
                                               FullName= "Ben Davies",
                                               Email ="ben.davies@cloudbass.com",
                                               Alergy = "Nut",
                                               Bared = "BTCC",
                                               NextOfKin ="DeSouza",
                                               PostNominals = "MSc, BSc",



                                               Users= new List<User>
                                               {
                                                   new User
                                                   {
                                                       Name = "Admin",
                                                       Password= Convert.ToBase64String(hash),
                                                       Salt = salt,
                                                   }
                                               }

                                           }

                                       }

                                    }
                };

                db.Counties.AddRange(counties);
                db.SaveChanges();



                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = "Camera Operator"

                    },

                    //new Role
                    //{
                    //    Name = "Rigger"

                    //}
                };

                db.Roles.AddRange(roles);
                db.SaveChanges();



                //var hasRoles = new List<HasRole>
                //{
                //    new HasRole
                //    {
                //        //EmployeeId=employeeId,
                //    //RoleId = roleId,
                //       Rate= 25,
                //       TotalDays = 3,
                //    }
                //};

                //db.HasRoles.AddRange(hasRoles);
                //db.SaveChanges();



                var clients = new List<Client>
                {
                    //new Client
                    //{
                    //    Name = "ITV",
                    //    ToContact = "Alexander Cooper",
                    //    Email = "alexander@gmail.com",
                    //    Address = "PO Box 12 london",
                    //    Tel = "02051122345"
                    //},
                    new Client
                    {
                        Name = "BBC",
                        ToContact = "Dave Cooper",
                        Email = "dave@gmail.com",
                        Address = "PO Box 12 nottingham",
                        Tel = "01151122345" ,

                                Jobs= new List<Job>
                                {
                                    //new Job
                                    //{
                                    //     Name = "SPL",
                                    //    Description = "friendly",
                                    //    Location = "Scotland celtic park",
                                    //    Coordinator = "Dixon",
                                    //    CreatedAt = DateTime.Parse("2020-04-18"),
                                    //    StartDate = DateTime.Parse("2020-05-10"),
                                    //    EndDate = DateTime.Parse("2020-05-13"),

                                    //    CommercialLead = "Francis Akai",

                                    //},

                                    new Job
                                    {
                                        Name = "MUTV",
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
                                                 Name="MUTV-Travel",
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
