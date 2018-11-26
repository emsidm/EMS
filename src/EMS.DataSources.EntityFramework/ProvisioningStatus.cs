using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EMS.Contracts.DataAccess;

namespace EMS.DataSources.EntityFramework
{
    public class ProvisioningStatus<TEntity> : IProvisioningStatus<TEntity>
    {
        [Key] public Guid RequestId { get; set; }
        public ProvisioningState State { get; set; }
        public string Message { get; set; }
        public IEnumerable<TEntity> Entities { get; set; }

        public override string ToString() => $"{RequestId} - {State.ToString()} ({Message})";
    }
}