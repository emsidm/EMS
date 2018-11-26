using System.Collections.Generic;
using EMS.Contracts.DataAccess;

namespace EMS.Contracts.Workers
{
    public interface IWorker
    {
        IEnumerable<IDataSource> Sources { get; }
        IEnumerable<IDataTarget> Targets { get; }

        ICollection<TJob> Jobs<TJob,TEntity>() where TJob : IJob<TEntity> where TEntity : IEntityBase;
    }
}