using System;
using System.Collections.Generic;
using EMS.Contracts.DataAccess;

namespace EMS.Contracts.Workers
{
    public interface IWorker
    {
        void AssignReadJob<TEntity>(ReadJob<TEntity> job) where TEntity : IEntityBase;
        void AssignWriteJob<TEntity>(WriteJob<TEntity> job) where TEntity : IEntityBase;
        
        event EventHandler<ReadJob<IEntityBase>> ReadJobAssigned;
        event EventHandler<WriteJob<IEntityBase>> WriteJobAssigned;
    }
}