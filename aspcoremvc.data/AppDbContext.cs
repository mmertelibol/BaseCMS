using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto;
using Data.Domain;
using Domain.User;
using Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.EntityFrameworkCore.DataEncryption;
using System.Text;
using System;
using Data.Domain.Panel;

namespace Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly CacheBase _cacheService;

      
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration, IHttpContextAccessor contextAccessor, CacheBase cacheService)
            : base(options)
        {
            
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _cacheService = cacheService;
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

        public DbSet<ActionLog> ActionLogs { get; set; }
        //public DbSet<Constant> Constants { get; set; }
       // public DbSet<EwsSetting> EwsSettings { get; set; }
       // public DbSet<EMailSetting> EMailSettings { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Page> Pages { get; set; }

        //PANEL
        public DbSet<StaticPage> StaticPage { get; set; }
       
        public DbSet<Setting> Setting { get; set; }
        public DbSet<News> News { get; set; }

       
        public DbSet<Slider> Slider { get; set; }
        public DbSet<PageComponent> PageComponent { get; set; }
        public DbSet<PageComponentCategory> PageComponentCategory { get; set; }

        public DbSet<NewsCategory> NewsCategory { get; set; }
        public DbSet<Address> Adress { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.UseEncryption(this._provider);

            //FK üzerinde cascade delete işlemi
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

           


            base.OnModelCreating(builder);

            builder.Entity<User>()
                .ToTable("Users", "User");

            builder.Entity<Role>()
                .ToTable("Roles", "User");

            builder.Entity<UserRole>()
                .ToTable("UserRoles", "User");

            builder.Entity<UserClaim>()
                .ToTable("UserClaims", "User");

            builder.Entity<UserLogin>()
                .ToTable("UserLogins", "User");

            builder.Entity<RoleClaim>()
                .ToTable("RoleClaims", "User");

            builder.Entity<UserToken>()
                .ToTable("UserTokens", "User");

            builder.Entity<User>()
                .HasOne(u => u.UpdatedBy)
                .WithMany();

            builder.Entity<User>()
                .HasOne(u => u.AddedBy)
                .WithMany();

            //builder.Entity<OperationalLog>()
            //   .HasOne(u => u.AddedBy)
            //   .WithMany();

            // table indexes

        }

        public override int SaveChanges()
        {
            Listeners.PreInsertListener(ChangeTracker);
            Listeners.UpdateListener(ChangeTracker);
            Listeners.DeleteListener(ChangeTracker);

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Listeners.PreInsertListener(ChangeTracker);
            Listeners.UpdateListener(ChangeTracker);
            Listeners.DeleteListener(ChangeTracker);

            return (await base.SaveChangesAsync(true, cancellationToken));
        }

      
    }
}