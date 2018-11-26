using System;
using System.Collections.Generic;
using EMS.DataSources.EntityFramework.Tests.Contexts;
using EMS.DataSources.EntityFramework.Tests.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataSources.EntityFramework.Tests.Helpers
{
    public static class DbConnectionHelpers
    {
        private static void InitializeConnection(out SqliteConnection connection)
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
        }

        public static void SeedContext<TContext>(out SqliteConnection connection)
            where TContext : DbContext
        {
            InitializeConnection(out connection);

            using (var context = (TContext) Activator.CreateInstance(typeof(TContext), connection))
            {
                context.Database.EnsureCreated();
                context.SaveChanges();
            }
        }
    }
}