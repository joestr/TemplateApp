using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateApp.Data.Contexts;

namespace TemplateApp.Core.Services
{
    /// <summary>
    /// Represents an entry point for database contexts to be registered.
    /// </summary>
    public static class DatabaseContextService
    {
        public static void RegisterDatabaseService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            var sqliteConnectionString = configuration.GetConnectionString("Sqlite");
            var msSqlServerConnectionString = configuration.GetConnectionString("MsSqlServer");
            var msSqlServerReadOnlyConnectionString = configuration.GetConnectionString("MsSqlServerReadOnly");

            var isSqlitePresent = sqliteConnectionString != null;
            var isMsSqlServerPresent = msSqlServerConnectionString != null;
            var isMsSqlServerReadOnlyPresent = msSqlServerReadOnlyConnectionString != null;

            if (isSqlitePresent)
            {
                serviceCollection.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlite(sqliteConnectionString));
            }
            else if (isMsSqlServerPresent)
            {
                serviceCollection.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(msSqlServerConnectionString));

                if (isMsSqlServerReadOnlyPresent)
                {
                    serviceCollection.AddDbContext<ReadOnlyDatabaseContext>(options =>
                        options.UseSqlServer(msSqlServerReadOnlyConnectionString));
                }
            }
        }
    }
}
