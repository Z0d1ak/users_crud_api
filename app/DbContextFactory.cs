using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using app.Data;

namespace app
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        private DbContextOptionsBuilder<DataContext> contextOptionsBuilder;

        private void InitializeBuilder()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("default");

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            contextOptionsBuilder = builder;
        }

        DataContext IDesignTimeDbContextFactory<DataContext>.CreateDbContext(string[] args)
        {
            if (contextOptionsBuilder is null)
                InitializeBuilder();

            return new DataContext(contextOptionsBuilder.Options);
        }
    }
}
