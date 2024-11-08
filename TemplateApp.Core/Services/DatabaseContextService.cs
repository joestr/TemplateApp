﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateApp.Data.Contexts;

namespace TemplateApp.Core.Services
{
    public class DatabaseContextService
    {
        public DatabaseContextService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var sqliteConnectionString = configuration.GetConnectionString("Sqlite");

            if (sqliteConnectionString != null)
            {
                serviceCollection.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlite(sqliteConnectionString);
                });
            }
        }
    }
}
