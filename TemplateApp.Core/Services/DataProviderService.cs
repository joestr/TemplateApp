﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TemplateApp.Core.Providers.Data;

namespace TemplateApp.Core.Services
{
    public class DataProviderService
    {
        public DataProviderService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<AuthorDataProvider>();
        }
    }
}
