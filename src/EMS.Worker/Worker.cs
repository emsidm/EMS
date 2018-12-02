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
        public void AssignReadJob<TEntity>(ReadJob<TEntity> job)
        {
            throw new NotImplementedException();
        }

        public void AssignWriteJob<TEntity>(WriteJob<TEntity> job)
        {
            throw new NotImplementedException();
        }
    }
}