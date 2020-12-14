using System.Threading;
using System.Threading.Tasks;
using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContextSystem : DbContext
    {
        public AppDbContextSystem(DbContextOptions<AppDbContextSystem> options) 
            : base(options)
        {
        }

        public DbSet<ActionLog> ActionLogs { get; set; }

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