using ASPNetBoilerplate.Web.Models.Configuration;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ASPNetBoilerplate.Web.Extensions;
using ASPNetBoilerplate.Web.CustomFilters;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using ASPNetBoilerplate.Domain.Interfaces;
using Dapper;
using ASPNetBoilerplate.Repository.Handlers;
using ASPNetBoilerplate.Repository.DapperClassMappers;

namespace ASPNetBoilerplate.Web
{
    /// <summary>
    /// The Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <exception cref="System.ArgumentNullException">environment</exception>
        public Startup(IWebHostEnvironment environment)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets WebApi Configuration
        /// </summary>
        /// <value>
        /// WebApi Configuration
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder" /></param>
        /// <param name="env"><see cref="IWebHostEnvironment" /></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.EnablePersistAuthorization();
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();

                // Global Exception Handler
                app.ConfigureExceptionHandler();
            }

            app.UseCustomCors();

            app.UseAuthentication();

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // setup Dapper Extensions
            DapperSetup.SetUpDapperExtensions();

            SqlMapper.AddTypeHandler(new DapperUriTypeHandler());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection" /></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // add MVC middleware with NullValueHandling.ignore in responses
            services.AddMvc().
                AddMvcOptions(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(typeof(ModelValidatorActionFilter));
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            /*
             * Suppress Model State validation behaviour with [ApiController] Attribute.
             * Use our own ModelValidatorActionFilter for BadRequest response.
             * https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1#default-badrequest-response
             */
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddRouting();

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            // configure AppSettings object using appsettings.json
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure Custom Options like Auth, Email etc.
            services.ConfigureCustomOptions(Configuration.GetSection("AppSettings"));

            AddCustomAuthConfiguration(services);

            //services.AddCustomAuthorizationHandler();

            // add CORS middleware
            services.AddCors();

            // add HttpContextAccessor, avoid Static/Global use of HttpContext
            services.AddHttpContextAccessor();

            // add the in-memory caching service
            services.AddMemoryCache();

            AddCustomServiceConfigurations(services);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ASPNetBoilerplate", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        /// <summary>
        /// Adds the custom authentication configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void AddCustomAuthConfiguration(IServiceCollection services)
        {
            // add Custom JWT based Authentication
            services.AddCustomAuthentication();

            // add Policy Based Authorization
            services.AddCustomAuthorization();
        }

        /// <summary>
        /// Adds the custom service configurations.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void AddCustomServiceConfigurations(IServiceCollection services)
        {
            services.AddSingleton<IConnectionResolver, ConnectionResolver>();

            services.ConfigureRepositories();

            services.ConfigureCustomUtilServices();

            services.ConfigureCustomServices();
        }
    }
}
