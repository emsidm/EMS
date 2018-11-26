using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataSources.EntityFramework
{
    public class EfDataContext<TContext> : IDataContext where TContext : DbContext
    {
        private readonly TContext _context;

        public EfDataContext(TContext context)
        {
            _context = context;
        }


        public IQueryable<TEntity> Entities<TEntity>() where TEntity : class, IEntityBase => _context.Set<TEntity>();
        public string Name { get; set; }


        public async Task<IProvisioningStatus<TEntity>> ProvisionAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntityBase
        {
            var dbSet = _context.Set<TEntity>();

            if (await _context.Set<TEntity>().AnyAsync(x => x.Id == entity.Id))
            {
                return await UpdateEntity(entity);
            }

            return await CreateEntity(entity);
        }

        private async Task<IProvisioningStatus<TEntity>> UpdateEntity<TEntity>(TEntity entity)
            where TEntity : class, IEntityBase
        {
            bool modified = false;
            _context.Set<TEntity>().Attach(entity);

            if (_context.Entry(entity).State == EntityState.Modified) modified = true;
            await _context.SaveChangesAsync();

            return new ProvisioningStatus<TEntity>
            {
                State = modified ? ProvisioningState.Modified : ProvisioningState.Unmodified,
                Entities = new[] {entity}
            };
        }

        private async Task<ProvisioningStatus<TEntity>> CreateEntity<TEntity>(TEntity entity)
            where TEntity : class, IEntityBase
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return new ProvisioningStatus<TEntity>
            {
                State = ProvisioningState.Created,
                Entities = new[] {entity}
            };
        }

        public async Task<IProvisioningStatus<TEntity>> BulkProvisionAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntityBase
        {
            var entitiesList = entities.ToList();
            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entitiesList);
                await _context.SaveChangesAsync();

                return new ProvisioningStatus<TEntity>
                {
                    State = ProvisioningState.Created,
                    Entities = entitiesList,
                };
            }
            catch (Exception e)
            {
                return new ProvisioningStatus<TEntity>
                {
                    State = ProvisioningState.Error,
                    Entities = entitiesList,
                    Message = e.Message
                };
            }
        }

        public async Task<IProvisioningStatus<TEntity>> GetProvisioningStatusAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntityBase
        {
            var dbSet = _context.Set<TEntity>();

            if (await dbSet.AnyAsync(x => x.Id == entity.Id))
            {
                return new ProvisioningStatus<TEntity>
                {
                    State = ProvisioningState.Unmodified,
                    Entities = new[] {entity}
                };
            }

            return new ProvisioningStatus<TEntity>
            {
                State = ProvisioningState.Inexistent,
                Entities = new[] {entity}
            };
        }

        public void Dispose() => _context.Dispose();
    }
}