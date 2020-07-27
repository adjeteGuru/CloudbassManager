using Cloudbass.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbass.Database
{
    public class CloudbassContext : DbContext
    {
        public CloudbassContext(DbContextOptions<CloudbassContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<HasRole> HasRoles { get; set; }

        // public DbSet<Role> Roles { get; set; }

        public DbSet<Crew> Crew { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crew>()
                .HasKey(t => new { t.JobId, t.EmployeeId });

            modelBuilder.Entity<Crew>()
                .HasOne(pt => pt.Job)
                .WithMany(p => p.CrewMembers)
                .HasForeignKey(pt => pt.JobId);

            modelBuilder.Entity<Crew>()
                .HasOne(pt => pt.Employee)
                .WithMany(t => t.JobInvoledIn)
                .HasForeignKey(pt => pt.EmployeeId);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
}
