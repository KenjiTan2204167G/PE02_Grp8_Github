using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Screentime_Tracker.Server.Models;

namespace Screentime_Tracker.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        public DbSet<ScreentimeEntry> ScreentimeEntries { get; set; }
        // Other DbSet properties...

        // If you have any additional configuration for your models, you can override the OnModelCreating method
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Example of additional configuration
            builder.Entity<ScreentimeEntry>(entity =>
            {
                // Additional configuration like setting table name, constraints, etc.
            });
        }
    }

}

