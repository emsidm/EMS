using System;
using System.Linq;

namespace EMS.Contracts.DataAccess
{
    public interface IDataSource : IDisposable
    {
        IQueryable<TEntity> Entities<TEntity>() where TEntity : class, IEntityBase;
        
        string Name { get; set; }
    }
}