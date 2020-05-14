
using Cloudbass.DataAccess.Repositories;
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Types;
using Cloudbass.Types.Jobs;
using Cloudbass.Utilities;
using Cloudbass.Utilities.Filters;
using CloudbassManager.Mutations;
using CloudbassManager.Queries;
using CloudbassManager.Subscriptions;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace CloudbassManager
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        internal static byte[] SharedSecret = Encoding.ASCII.GetBytes("LVmlpXCs7UeGCiIgqcnxW9VwHdQDHleAKMTWzX176B2fG10N3rG5UpRyytMYyLliCiAxsqILYqof6IIZrKG4eAtNQDbuRYxpn8uE9SweVgIKOKILtaMnW2iC");


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // configure jwt authentication           

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings objects
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userRepository.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //this allows to instanciate a new object of the repository to be provided to
            //every services or controller
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddControllers();
            services.AddCors();

            // this register the logger with the dependency injection
            services.AddLogging();


            //db connection strings
            //services.AddDbContext<CloudbassContext>(options =>
            //   options.UseSqlServer(Configuration["ConnectionStrings:CloudbassDb"]));


            //// use sql server db in production and sql db in development
            if (_env.IsProduction())
                services.AddDbContext<CloudbassContext>();
            else
                services.AddDbContext<CloudbassContext, CloudbassContext>(options =>
               options.UseSqlServer(_configuration["ConnectionStrings:CloudbassDb"]));


            services.AddSingleton<UserType>();
            services.AddSingleton<ClientType>();
            services.AddSingleton<JobType>();

            //this is to record the job not found exception
            services.AddErrorFilter<JobNotFoundExceptionFilter>();

            //This adds the GraphQL schema and the execution engine to the dependency injection 
            //which is Registering services / repositories
            services
                // this enables you to use DataLoader in your resolvers.
                .AddDataLoaderRegistry()
                .AddInMemorySubscriptions()
                .AddGraphQL(sp =>
                    SchemaBuilder.New()
                        .AddServices(sp)
                        .AddQueryType(d => d.Name("Query"))
                        .AddType<UserQuery>()
                        .AddType<Query>()

                        .AddMutationType(d => d.Name("Mutation"))
                        //.AddType<LoginMutation>()
                        .AddType<UserMutations>()
                        .AddType<JobMutations>()
                        .AddType<ClientMutations>()
                        .AddSubscriptionType(d => d.Name("Subscription"))
                        .AddType<UserSubscriptions>()
                        .AddAuthorizeDirectiveType()
                        .Create(),

                    // Registering schema types and so on                   
                    new QueryExecutionOptions { ForceSerialExecution = true });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CloudbassContext db)
        {// IServiceProvider provider
         //var applicationServices = app.ApplicationServices;
         //if (env.IsDevelopment())
         //{
         //    app.UseDeveloperExceptionPage();
         //}


            app.UseAuthentication();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();



            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            //add the GraphQL middleware to the pipeline so the server can serve GraphQL requests
            app
                .UseWebSockets()
                .UseGraphQL()
                .UsePlayground()
                .UseVoyager();

            db.EnsureSeedData();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
