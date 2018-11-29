//using System.Collections.Generic;
//using EMS.Contracts.DataAccess;
//using EMS.Contracts.Workers;
//using Microsoft.Extensions.Options;
//
//namespace EMS.Synchronizer
//{
//    public class SimpleSynchronizer : ISynchronizer
//    {
//        public SimpleSynchronizer(IOptions<SynchronizerConfiguration> configuration)
//        {
////            DataSources = configuration.Value.DataSources;
////            DataTargets = configuration.Value.DataTargets;
//        }
//
////        public IDictionary<string, IEnumerable<DataTargetConfiguration>> DataTargets { get; }
//
////        public IDictionary<string, IEnumerable<DataSourceConfiguration>> DataSources { get; }
//
////        
////        public void AssignReadJob<T>(ReadJob<T> job) where T : IEntityBase
////        {
////            ReadWorkers[job.Source].AssignReadJob(job);
////        }
////
////        public void AssignWriteJob<T>(WriteJob<T> job) where T : IEntityBase
////        {
////            WriteWorkers[job.Target].AssignWriteJob(job);
////        }
//        public void AssignReadJob<T>(ReadJob<T> job) where T : IEntityBase
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public void AssignWriteJob<T>(WriteJob<T> job) where T : IEntityBase
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}