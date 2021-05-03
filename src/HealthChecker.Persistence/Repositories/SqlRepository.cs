using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthChecker.Persistence.Contracts;
using HealthChecker.Persistence.Dtos;
using HealthChecker.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthChecker.Persistence.Repositories
{


    /// <summary>
    /// Generic repository pattern implementation for CRUD operations on Sql Server databases. 
    /// Responsible for operation such as filtering soft-deleted records and mapping between entites and data transfer objects.
    /// 
    /// <para/>
    /// 
    /// Implements : <see cref="ICrudRepository{TEntity, TDto}"/>
    /// Implements : <see cref="ITargetRepository"/>
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntityWithCommonProperties"/> interface.</typeparam>
    /// <typeparam name="TDto">Entity class that implements <see cref="IDtoWithCommonProperties"/> interface.</typeparam>
    public class SqlRespository<TEntity, TDto> : ICrudRepository<TEntity, TDto>, ITargetRepository
        where TDto : class, IDtoWithCommonProperties
        where TEntity : class, IEntityWithCommonProperties
    {
        private DbContext _dbContext;
        private readonly IMapper _mapper;
        private DbSet<TEntity> _entity;





        /// <summary>
        /// Constructor with <see cref="DbContextOptions{T}"/> parameter that initializes <see cref="AppDbContext"/> with incoming options.
        /// 
        /// <para/>
        /// 
        /// Dependencies : <see cref="IMapper"/>
        /// </summary>
        public SqlRespository(DbContextOptions<AppDbContext> options, IMapper mapper)
        {
            _dbContext = new AppDbContext(options);
            _entity = _dbContext.Set<TEntity>();
            _mapper = mapper;
        }





        /// <summary>
        /// Retrieves all entity records.
        /// </summary>
        public async Task<IEnumerable<TDto>> GetAsync()
        {
            try
            {
                return await _entity
                    .Where(x => x.IsDeleted == false)
                    .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }





        /// <summary>
        /// Retrieves the entity with the specified Id.
        /// </summary>
        /// <param name="Id">Id of the entity to be retrieved.</param>
        public async Task<TDto> GetAsync(int Id)
        {
            try
            {
                TEntity entity = await _entity.FindAsync(Id);
                TDto dto = _mapper.Map<TDto>(entity);

                return dto;
            }
            catch
            {
                throw;
            }
        }





        /// <summary>
        /// Retrieves entities with specified filters.
        /// </summary>
        public async Task<IEnumerable<TDto>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return await _entity
                    .Where(x => x.IsDeleted == false)
                    .Where(filter)
                    .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }





        /// <summary>
        /// Inserts new entity.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        public async Task<TDto> InsertAsync(TDto entityDto)
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(entityDto);

                DateTime datetimeNow = DateTime.Now;

                entity.IsDeleted = false;
                entity.DateAdded = datetimeNow;
                entity.DateModified = datetimeNow;
                _entity.Add(entity);

                await Save();

                entityDto.Id = entity.Id;

                return entityDto;
            }
            catch
            {
                throw;
            }
        }





        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        public async virtual Task<TDto> UpdateAsync(TDto entityDto)
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(entityDto);

                var attachedEntity = _entity.Local.FirstOrDefault(entry => entry.Id.Equals(entityDto.Id));

                if (attachedEntity != null)
                {
                    _dbContext.Entry(attachedEntity).State = EntityState.Detached;
                }

                entity.DateModified = DateTime.Now;

                _dbContext.Entry(entity).State = EntityState.Modified;

                await Save();

                return entityDto;
            }
            catch
            {
                throw;
            }
        }






        /// <summary>
        /// Soft-deletes the entity.
        /// </summary>
        /// <param name="entity">Entity to be soft-deleted.</param>
        public async Task<TDto> DeleteAsync(TDto entityDto)
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(entityDto);

                var attachedEntity = _entity.Local.FirstOrDefault(entry => entry.Id.Equals(entityDto.Id));

                if (attachedEntity != null)
                {
                    _dbContext.Entry(attachedEntity).State = EntityState.Detached;
                }

                entity.IsDeleted = true;
                entity.DateModified = DateTime.Now;

                _dbContext.Entry(entity).State = EntityState.Modified;

                await Save();

                return entityDto;
            }
            catch
            {
                throw;
            }
        }





        /// <summary>
        /// Wrapper for DbContex.SaveCahngesAsync() method.
        /// </summary>
        private async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This method only updates some of the properties of the Target entity.
        /// </summary>
        /// <param name="targetDto">DTO form of Target entity.</param>
        /// <returns>Task</returns>
        public async Task UpdatePartially(TargetDto targetDto)
        {
            var db = new AppDbContextFactory().CreateDbContext(new string[0]);
            var target = await db.Targets.Where(x => x.Id == targetDto.Id).FirstOrDefaultAsync();
            target.DateModified = DateTime.Now;
            target.MonitoringInterval = targetDto.MonitoringInterval;
            target.Name = targetDto.Name;
            target.Url = targetDto.Url;
            await db.SaveChangesAsync();
        }





        //private bool disposed = false;

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            if (_dbContext != null)
        //            {
        //                _dbContext.Dispose();
        //                _dbContext = null;
        //            }
        //        }

        //        disposed = true;
        //    }
        //}
    }
}
