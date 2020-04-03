using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System.IO;
using System.Text;

namespace Cloudbass.Database
{
   public class CloudbassContextFactory : IDesignTimeDbContextFactory<CloudbassContext>
    {
        public CloudbassContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") //just reference
                .Build();//and call the build

            //this instance created is needed in order to pass the connection string to the sql server instance
            var builder = new DbContextOptionsBuilder<CloudbassContext>();
            var connectionString = configuration.GetConnectionString("CloudbassDb");
            builder.UseSqlServer(connectionString);
            return new CloudbassContext(builder.Options);
        }
    }
}
