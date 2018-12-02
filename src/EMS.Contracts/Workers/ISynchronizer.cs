using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EMS.Contracts.DataAccess;

namespace EMS.Contracts.Workers
{
    public interface ISynchronizer
    {
        void AssignReadJob<T>(ReadJob<T> job);
        void AssignWriteJob<T>(WriteJob<T> job);
    }

    public interface IJob<T>
    {
        IEnumerable<T> Entities { get; set; }
    }

    public class ReadJob<T> : IJob<T>
    {
        public IDataSource Source { get; set; }
        public IEnumerable<T> Entities { get; set; }
    }

    public class WriteJob<T> : IJob<T>
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