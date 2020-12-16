using Business.Services;
using Business.Services.Interfaces;
using Common;
using Common.Helpers;
using Common.Resources;
using Data;
using Web.Models;
using Domain.User;
using GreenPipes;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Common;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Business.Services.Panel.Interfaces;
using Business.Services.Panel;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void DbContextAdder<T>(IServiceCollection services, string conn, string assemblyNameDB) where T : DbContext
        {
            services.AddDbContext<T>(options =>
            {
                if (bool.Parse(Configuration["AppConfig:UseInMemoryDB"]))
                    options.UseInMemoryDatabase(new Guid().ToString());
                else
                    options.UseSqlServer(HttpHelper.ConnStr, sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(10200);
                        sqlServerOptions.MigrationsAssembly(assemblyNameDB);
                    });
                if (bool.Parse(Configuration["Serilog:LogSQLParameters"]))
                    options.EnableSensitiveDataLogging(true);
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            HttpHelper.ConnStr = Configuration.GetConnectionString(Configuration["AppConfig:ActiveConnection"]);
            string assemblyNameDb = typeof(AppDbContext).Namespace;
            DbContextAdder<AppDbContext>(services, HttpHelper.ConnStr, assemblyNameDb);
            DbContextAdder<AppDbContextSystem>(services, HttpHelper.ConnStr, assemblyNameDb);

            services.AddMemoryCache();
            services.AddSingleton<ITicketStore, MemoryCacheTicketStore>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings  
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                // Lockout settings  
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                // User settings  
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings  
                options.Cookie.HttpOnly = true;
                //options.Cookie.Expiration = TimeSpan.FromDays(30);
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                //options.SessionStore = services.BuildServiceProvider().GetService<ITicketStore>();
            });

            if (bool.Parse(Configuration["AppConfig:HangFireOnMemory"]))
                services.AddHangfire(opts => opts.UseMemoryStorage());
            else
                services.AddHangfire(opts => opts.UseSqlServerStorage(HttpHelper.ConnStr));

            services.AddSingleton<LocService>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddCors();

            //PANELSERVICES
            //PANELSERVICES
            services.AddScoped<INewsService, NewsService>();


            //services.AddMvc().AddNewtonsoftJson();
            //services.AddMvc(option => option.EnableEndpointRouting = false)
            //    .AddNewtonsoftJson();

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                });

            //services.Configure<RequestLocalizationOptions>(
            //    options =>
            //    {
            //        var supportedCultures = new List<CultureInfo>
            //        {
            //            new CultureInfo("tr-TR"),
            //            new CultureInfo("en-US")
            //        };
            //    });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddSignalR();

            if (bool.Parse(Configuration["AppConfig:UseRedis"]))
                services.AddSingleton<CacheBase, RedisCacheManager>();
            else
                services.AddSingleton<CacheBase, AppMemoryCache>();

            services.AddResponseCaching();

            #region Business Services


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGeneralService, GeneralService>();



            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });



            #endregion
            //Rabbitmq
            //services.AddScoped<SendMessageConsumer>();
            //services.AddMassTransit(c =>
            //{
            //    c.AddConsumer<SendMessageConsumer>();
            //});

            //services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(
            //  cfg =>
            //  {
            //      var host = cfg.Host(new Uri(Configuration["RabbitMQ:URI"]), b => { b.Username(Configuration["RabbitMQ:Username"]); b.Password(Configuration["RabbitMQ:Password"]); });
            //      cfg.ReceiveEndpoint(host, Configuration["RabbitMQ:DefaultQueue"], e =>
            //      {
            //          e.PrefetchCount = 16;
            //          e.UseMessageRetry(x => x.Interval(2, 100));

            //          e.LoadFrom(provider);
            //          EndpointConvention.Map<SendMessageConsumer>(e.InputAddress);
            //      });
            //  }));

            //services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            //services.AddSingleton<IHostedService, MassTransitBusService>();
        }

        public void Configure(IApplicationBuilder app,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment env,
            IHttpContextAccessor httpContextAccessor,
            IRecurringJobManager recurringJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            HttpHelper.Configure(httpContextAccessor, Configuration);


            var ci = new CultureInfo("tr-TR")
            {
                NumberFormat =
                {
                    NumberDecimalSeparator = ",",
                    NumberGroupSeparator = "."
                }
            };

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("tr-TR"),
            };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ci),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
                //SupportedCultures = new List<CultureInfo> {ci},
                //SupportedUICultures = new List<CultureInfo> {ci}
            };

            var cookieProvider = localizationOptions.RequestCultureProviders
                .OfType<CookieRequestCultureProvider>()
                .First();

            cookieProvider.CookieName = "UserCultureInfo";

            app.UseRequestLocalization(localizationOptions);

            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            //app.UseCors(
            //    options => options.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //);

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            app.UseHangfireDashboard("/hfdashboard", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter(Configuration) }
            });

            app.UseHangfireServer();

            app.UseSignalR(routes =>
            {
                routes.MapHub<MainHub>("/mainHub");

            });

            app.UseMvc(routes =>
            {


                routes.MapRoute(
                    name: "default",
                    template: "{controller=HomePage}/{action=Index}/{id?}");
            });


        }
    }

    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {

        public IConfiguration Configuration { get; }


        public HangFireAuthorizationFilter(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            var header = httpContext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(header))
            {
                SetChallengeResponse(httpContext);
                return false;
            }

            var authValues = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(header);

            if (!"Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
            {
                SetChallengeResponse(httpContext);
                return false;
            }

            var parameter = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
            var parts = parameter.Split(':');

            if (parts.Length < 2)
            {
                SetChallengeResponse(httpContext);
                return false;
            }

            var username = parts[0];
            var password = parts[1];

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                SetChallengeResponse(httpContext);
                return false;
            }


            if (username == Configuration["AppConfig:HangFireUsername"] && password == Configuration["AppConfig:HangFirePassword"])
            {
                return true;
            }

            SetChallengeResponse(httpContext);
            return false;
        }

        private void SetChallengeResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
            httpContext.Response.WriteAsync("Authentication is required.");
        }
    }

    public class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;
        public HangfireActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public override object ActivateJob(System.Type jobType) => _serviceProvider.GetService(jobType);
    }

    public class NoAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context) => HttpHelper.HttpContext.User.Identity.IsAuthenticated;
    }
}