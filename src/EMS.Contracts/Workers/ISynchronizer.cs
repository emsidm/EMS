using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EMS.Contracts.DataAccess;

namespace EMS.Contracts.Workers
{
    public interface ISynchronizer
    {
        void AssignReadJob<T>(ReadJob<T> job) where T : IEntityBase;
        void AssignWriteJob<T>(WriteJob<T> job) where T : IEntityBase;
    }

    public interface IJob<T> where T : IEntityBase
    {
        IEnumerable<T> Entities { get; set; }
    }

    public abstract class ReadJob<T> : IJob<T> where T : IEntityBase
    {
        public IDataSource Source { get; set; }
        public IEnumerable<T> Entities { get; set; }
    }

    public abstract class WriteJob<T> : IJob<T> where T : IEntityBase
    {
        public IDataTarget Target { get; set; }
        public IEnumerable<T> Entities { get; set; }
    }

    public enum JobType
    {
        Read,
        Write
    }
}