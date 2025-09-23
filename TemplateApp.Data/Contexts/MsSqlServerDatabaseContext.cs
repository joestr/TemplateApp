using Microsoft.EntityFrameworkCore;

namespace TemplateApp.Data.Contexts
{
    public class MsSqlServerDatabaseContext : DatabaseContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder = optionsBuilder.UseSqlServer();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
