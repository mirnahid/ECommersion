using ECommersionAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommersionAPI.Persistence
{
    internal class DesignTImeDbContextFactory : IDesignTimeDbContextFactory<ECommersionAPIDbContext>
    {
        public ECommersionAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ECommersionAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);

            return new(dbContextOptionsBuilder.Options);
        }
    }
}
