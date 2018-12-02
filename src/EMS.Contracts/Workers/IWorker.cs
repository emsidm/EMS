using System;
using System.Collections.Generic;
using EMS.Contracts.DataAccess;

namespace EMS.Contracts.Workers
{
    public interface IWorker
    {
        void AssignReadJob<TEntity>(ReadJob<TEntity> job);
        void AssignWriteJob<TEntity>(WriteJob<TEntity> job);
    }
}