using System;
using EMS.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.DataSources.EntityFramework
{
    public static class Extensions
    {
        public static void AddDataSource<T>(this IServiceCollection services, 
            Action<DbContextOptionsBuilder> options) where T : DbContext
        {
            services.AddDbContext<T>(options);
            services.AddTransient<IDataSource, EfDataContext<T>>();
        }
        
        public static void AddDataTarget<T>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options) where T : DbContext
        {
            services.AddDbContext<T>(options);
            services.AddTransient<IDataTarget, EfDataContext<T>>();
        }
        public static void AddDataContext<T>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options) where T : DbContext
        {
            services.AddDbContext<T>(options);
            services.AddTransient<IDataSource, EfDataContext<T>>();
            services.AddTransient<IDataTarget, EfDataContext<T>>();
        }
    }
}