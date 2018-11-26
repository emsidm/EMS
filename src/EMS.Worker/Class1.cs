using System;
using System.Collections.Generic;
using System.Linq;
using EMS.Contracts.DataAccess;
using EMS.Contracts.Workers;

namespace EMS.Worker
{
    public class Worker : IWorker
    {
        private Dictionary<Type, ObservableQueue<IJob<IEntityBase>>> _queues;

        public Worker()
        {
        }

        private void OnItemAdded()
        {
            JobAssigned?.Invoke(this, null);
        }

        public IEnumerable<IDataSource> Sources { get; }
        public IEnumerable<IDataTarget> Targets { get; }

        public ICollection<TJob> Jobs<TJob, TEntity>() where TJob : IJob<TEntity> where TEntity : IEntityBase =>
            _queues[typeof(TJob)] as ObservableQueue<TJob>;

        public event EventHandler JobAssigned;

        public TJob GetJob<TJob, TEntity>() where TJob : class, IJob<TEntity> where TEntity : IEntityBase =>
            (Jobs<TJob, TEntity>() as ObservableQueue<TJob>)?.Dequeue();
    }
}