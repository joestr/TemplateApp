using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Data.Entities;

namespace TemplateApp.Data.Contexts
{
    public class ReadOnlyDatabaseContext : DatabaseContext
    {
        public ReadOnlyDatabaseContext()
        {
        }

        public ReadOnlyDatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
