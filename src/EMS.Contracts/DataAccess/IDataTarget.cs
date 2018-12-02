using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EMS.Contracts.DataAccess
{
    public interface IDataTarget : IDisposable
    {
        Task<IProvisioningStatus<TEntity>> ProvisionAsync<TEntity>(TEntity entity) where TEntity : class;

        Task<IProvisioningStatus<TEntity>> BulkProvisionAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;
        
        Task<IProvisioningStatus<TEntity>> DeprovisionAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task<IProvisioningStatus<TEntity>> BulkDeprovisionAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;
        
        Task<IProvisioningStatus<TEntity>> GetProvisioningStatusAsync<TEntity>(TEntity entity)
            where TEntity : class;
        
        string Name { get; set; }
    }

    public interface IProvisioningStatus<TEntity>
    {
        [Key] Guid RequestId { get; set; }
        ProvisioningState State { get; set; }
        string Message { get; set; }
        IEnumerable<TEntity> Entities { get; set; }
    }

    public enum ProvisioningState
    {
        Created,
        Updated,
        Unmodified,
        Inexistent,
        Error,
        Deleted
    }
}