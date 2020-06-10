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
        public DbSet<HasRole> HasRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        // public DbSet<County> Counties { get; set; }
        //public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crew>()
                .HasKey(t => new { t.JobId, t.HasRoleId });

            modelBuilder.Entity<Crew>()
                .HasOne(pt => pt.Job)
                .WithMany(p => p.CrewMembers)
                .HasForeignKey(pt => pt.JobId);

            modelBuilder.Entity<Crew>()
                .HasOne(pt => pt.HasRole)
                .WithMany(t => t.CrewMembers)
                .HasForeignKey(pt => pt.HasRoleId);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<HasRole>()
        //        .HasKey(t => new { t.UserId, t.RoleId });

        //    modelBuilder.Entity<HasRole>()
        //        .HasOne(pt => pt.User)
        //        .WithMany(p => p.HasRoles)
        //        .HasForeignKey(pt => pt.EmployeeId);

        //    modelBuilder.Entity<HasRole>()
        //        .HasOne(pt => pt.Role)
        //        .WithMany(t => t.HasRoles)
        //        .HasForeignKey(pt => pt.RoleId);
        //}

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
