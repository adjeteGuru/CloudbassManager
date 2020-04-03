
using Cloudbass.DataAccess.Repositories;
using Cloudbass.DataAccess.Repositories.Contracts;
using Cloudbass.Database;
using Cloudbass.Types;
using CloudbassManager.Queries;
using CloudbassManager.Schema;
//using GraphiQl;

//using GraphQL;
using GraphQL.Types;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
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
            //ConfigureAuthenticationServices(services);

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddControllers();
            services.AddCors();

            // this register the logger with the dependency injection 

            services.AddLogging();

            //db connection strings

            services.AddDbContext<CloudbassContext>(options =>
               options.UseSqlServer(Configuration["ConnectionStrings:CloudbassDb"]));


            //when registering as transient a new instance will be provided to every service 

            services.AddTransient<IUserRepository, UserRepository>();
            // services.AddScoped<IUserRepository, UserRepository>();

            // Add GraphQL Services
            //services
            //    .AddDataLoaderRegistry()
            //   // .AddInMemorySubscriptions()
            //    .AddGraphQL(sp =>
            //        SchemaBuilder.New()
            //            .AddServices(sp)
            //            .AddQueryType(d => d.Name("Query"))
            //            .AddType<UserQuery>()
            //            //.AddMutationType(d => d.Name("Mutation"))
            //            //.AddType<LoginMutation>()
            //            //.AddType<UserMutations>()
            //            //.AddSubscriptionType(d => d.Name("Subscription"))
            //            //.AddType<UserSubcriptions>()
            //            .AddAuthorizeDirectiveType()
            //            .Create(),
            //        new QueryExecutionOptions { ForceSerialExecution = true });


            //This adds the GraphQL schema and the execution engine to the dependency injection.

            services.AddGraphQL(sp => SchemaBuilder.New()
              .AddServices(sp)
               .AddAuthorizeDirectiveType()
              .AddQueryType<QueryType>()
                 .Create());


            //  services.AddDbContext<CloudbassContext>(options =>
            //options.UseSqlite(Configuration.GetConnectionString("CloudbassContext")));

            //to register IDocument as singleton
            // services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<UserType>();

            // this enables you to use DataLoader in your resolvers.
            services.AddDataLoaderRegistry();

            //// Add GraphQL Services
            //services.AddGraphQLSchema(SchemaBuilder.New()
            //    // enable for authorization support
            //    //.AddAuthorizeDirectiveType()
            //    .AddQueryType<UserQuery>()
            //   .ModifyOptions(o => o.RemoveUnreachableTypes = true));

            //var sp = services.BuildServiceProvider();
            //services.AddSingleton<ISchema>(new CloudbassSchema(new FuncDependencyResolver(type => sp.GetService(type))));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CloudbassContext db)
        {// IServiceProvider provider
            //var applicationServices = app.ApplicationServices;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //this function in order to open an app in GraphQL(api request helper such as postman)
            //app.UseGraphiQl();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();



            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app
                .UseWebSockets()
                .UseGraphQL(/*"/graphql"*/)
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
