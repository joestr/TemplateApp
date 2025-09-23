using Microsoft.EntityFrameworkCore;

namespace TemplateApp.Data.Contexts
{
    public class SqliteDatabaseContext : DatabaseContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder = optionsBuilder.UseSqlite();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
