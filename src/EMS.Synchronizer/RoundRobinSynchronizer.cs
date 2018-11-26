using System;
using System.Collections.Generic;
using System.Linq;
using EMS.Contracts.DataAccess;
using EMS.Contracts.Workers;

namespace EMS.Synchronizer
{
    public class RoundRobinSynchronizer : ISynchronizer
    {
        private IEnumerator<IDataSource> _lastDataSource;
        private IEnumerator<IDataTarget> _lastDataTarget;


        public IEnumerable<IDataSource> Sources { get; set; }
        public IEnumerable<IDataTarget> Targets { get; set; }
        public IEnumerable<IWorker> Workers { get; set; }
        public void AssignReadJob<T>(ReadJob<T> job) where T : IEntityBase
        {
            if (_lastDataSource == null ||!_lastDataSource.MoveNext()) _lastDataSource = Sources.GetEnumerator();
            Workers.FirstOrDefault(x => x.Sources.Contains(_lastDataSource.Current));
        }

        public void AssignWriteJob<T>(WriteJob<T> job) where T : IEntityBase
        {
            if (_lastDataTarget == null || !_lastDataTarget.MoveNext()) _lastDataTarget = Targets.GetEnumerator();
            Workers.FirstOrDefault(x => x.Targets.Contains(_lastDataTarget.Current));
        }
    }
}