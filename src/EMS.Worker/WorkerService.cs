using System;
using EMS.Contracts.Workers;

namespace EMS.Worker
{
    public interface IWorkerService
    {
        IWorker Worker { get; }
    }

    public class WorkerService : IWorkerService
    {
        public IWorker Worker { get; }

        public WorkerService(IWorker worker)
        {
            Worker = worker;
        }
    }
}