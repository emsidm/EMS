using System;
using System.Collections.Generic;
using System.Linq;
using EMS.Contracts.DataAccess;
using EMS.Contracts.Workers;
using Microsoft.Extensions.Options;

namespace EMS.Worker
{
    public class Worker : IWorker
    {
        public void AssignReadJob<TEntity>(ReadJob<TEntity> job) where TEntity : IEntityBase
        {
            ReadJobAssigned?.Invoke(this, job as ReadJob<IEntityBase>);
        }

        public void AssignWriteJob<TEntity>(WriteJob<TEntity> job) where TEntity : IEntityBase
        {
            WriteJobAssigned?.Invoke(this, job as WriteJob<IEntityBase>);
        }

        public event EventHandler<ReadJob<IEntityBase>> ReadJobAssigned;
        public event EventHandler<WriteJob<IEntityBase>> WriteJobAssigned;
    }
}