
using Cloudbass.DataAccess.Repositories;
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Types;
using Cloudbass.Types.Counties;
using Cloudbass.Types.Crews;
using Cloudbass.Types.Employees;
using Cloudbass.Types.HasRoles;
using Cloudbass.Types.Jobs;
using Cloudbass.Types.Roles;
using Cloudbass.Types.Schedules;
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
using System.Text;
using System.Threading.Tasks;

namespace CloudbassManager
{
    public class Startup
    {
        internal static byte[] SharedSecret = Encoding.ASCII.GetBytes("LVmlpXCs7UeGCiIgqcnxW9VwHdQDHleAKMTWzX176B2fG10N3rG5UpRyytMYyLliCiAxsqILYqof6IIZrKG4eAtNQDbuRYxpn8uE9SweVgIKOKILtaMnW2iC");
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { // configure jwt authentication
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(SharedSecret),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>

                        {
                            if (context.HttpContext.Request.Query.ContainsKey("token"))
                            {
                                context.Token = context.HttpContext.Request.Query["token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            //this allows to instanciate a new object of the repository to be provided to
            //every services or controller
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IHasRoleRepository, HasRoleRepository>();
            services.AddTransient<ICountyRepository, CountyRepository>();
            services.AddTransient<ICrewRepository, CrewRepository>();

            services.AddScoped<UserRepository>();
            services.AddScoped<ClientRepository>();
            services.AddScoped<JobRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<ScheduleRepository>();
            services.AddScoped<HasRoleRepository>();
            services.AddScoped<CrewRepository>();
            services.AddScoped<CountyRepository>();

            services.AddControllers();
            services.AddCors();



            // this register the logger with the dependency injection
            services.AddLogging();

            //db connection strings
            services.AddDbContext<CloudbassContext>(options =>
               options.UseSqlServer(Configuration["ConnectionStrings:CloudbassDb"]));
            services.AddSingleton<UserType>();
            services.AddSingleton<ClientType>();
            services.AddSingleton<JobType>();
            services.AddSingleton<ScheduleType>();
            services.AddSingleton<EmployeeType>();
            services.AddSingleton<RoleType>();
            services.AddSingleton<CountyType>();
            services.AddSingleton<HasRoleType>();
            services.AddSingleton<CrewType>();
            services.AddSingleton<QueryType>();

            //this is to record the job not found exception
            services.AddErrorFilter<JobNotFoundExceptionFilter>();


            // this enables you to use DataLoader in your resolvers.
            services.AddDataLoaderRegistry();

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
                        .AddType<EmployeeQuery>()
                        .AddType<Query>()
                        .AddMutationType(d => d.Name("Mutation"))
                        .AddType<LoginMutation>()
                        .AddType<LogoutMutation>()
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
