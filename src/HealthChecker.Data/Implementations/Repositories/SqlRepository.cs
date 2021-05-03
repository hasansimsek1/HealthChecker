using AutoMapper;
using HealthChecker.Data.Definitions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthChecker.Data.Implementations.Repositories
{
    /// <summary>
    /// Generic repository pattern implementation for CRUD operations on Sql Server databases. 
    /// Responsible for filtering soft-deleted records and mapping between entites and data transfer objects.
    /// 
    /// <para/>
    /// 
    /// Implements : <see cref="ICrudRepository{TEntity, TDto}"/>
    /// </summary>
    /// <typeparam name="TEntity">Entity class that implements <see cref="IEntityWithCommonProperties"/> interface.</typeparam>
    /// <typeparam name="TDto">Entity class that implements <see cref="IDtoWithCommonProperties"/> interface.</typeparam>


    internal class SqlRespository<TEntity, TDto> : ICrudRepository<TEntity, TDto>
        where TDto : class, IDtoWithCommonProperties
        where TEntity : class, IEntityWithCommonProperties
    {
        private DbContext _dbContext;
        private readonly IMapper _mapper;
        private DbSet<TEntity> _entity;

        /// <summary>
        /// Constructor that initializes new instance of <see cref="DbContext"/> using <see cref="AppDbContextFactory"/>.
        /// This constructor is being used by the Wpf application.
        /// 
        /// <para/>
        /// 
        /// Dependencies : <see cref="IMapper"/>
        /// </summary>
        public SqlRespository(IMapper mapper)
        {
            _dbContext = new AppDbContextFactory().CreateDbContext(new string[0]);
            _entity = _dbContext.Set<TEntity>();
            _mapper = mapper;
        }

        /// <summary>
        /// Constructor with <see cref="DbContextOptions{T}"/> parameter that initializes <see cref="AppDbContext"/> with incoming options.
        /// This constructor is being used by the AspNetCoreMvc application.
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
        public async Task<Result<List<TDto>>> GetAsync()
        {
            try
            {
                return new Result<List<TDto>>()
                {
                    Data = await _entity.Where(x => x.IsDeleted == false).ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync(),
                };
            }
            catch (Exception ex)
            {
                return new Result<List<TDto>>(ex, ExceptionLocations.SqlRepositoryGet);
            }
        }

        /// <summary>
        /// Retrieves the entity with the specified Id.
        /// </summary>
        /// <param name="Id">Id of the entity to be retrieved.</param>
        public async Task<Result<TDto>> GetAsync(int Id)
        {
            try
            {
                TEntity entity = await _entity.FindAsync(Id);
                TDto dto = _mapper.Map<TDto>(entity);

                return new Result<TDto>()
                {
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new Result<TDto>(ex, ExceptionLocations.SqlRepositoryGetById);
            }
        }

        /// <summary>
        /// Retrieves entities with specified filters.
        /// </summary>
        public async Task<Result<List<TDto>>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return new Result<List<TDto>>()
                {
                    Data = await _entity.Where(x => x.IsDeleted == false).Where(filter).ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync()
                };
            }
            catch (Exception ex)
            {
                return new Result<List<TDto>>(ex, ExceptionLocations.SqlRepositoryGetByFilter);
            }
        }


        /// <summary>
        /// Inserts new entity.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        public async Task<Result<TDto>> InsertAsync(TDto entityDto)
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(entityDto);

                DateTime datetimeNow = DateTime.Now;

                entity.IsDeleted = false;
                entity.AddedDate = datetimeNow;
                entity.ModifiedDate = datetimeNow;
                _entity.Add(entity);

                await Save();

                entityDto.Id = entity.Id;

                return new Result<TDto>(entityDto);
            }
            catch (Exception ex)
            {
                return new Result<TDto>(ex, ExceptionLocations.SqlRepositoryInsert, entityDto);
            }
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        public async Task<Result<TDto>> UpdateAsync(TDto entityDto)
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(entityDto);

                var attachedEntity = _entity.Local.FirstOrDefault(entry => entry.Id.Equals(entityDto.Id));

                if (attachedEntity != null)
                {
                    _dbContext.Entry(attachedEntity).State = EntityState.Detached;
                }

                entity.ModifiedDate = DateTime.Now;

                _dbContext.Entry(entity).State = EntityState.Modified;

                await Save();

                return new Result<TDto>(entityDto);
            }
            catch (Exception ex)
            {
                return new Result<TDto>(ex, ExceptionLocations.SqlRepositoryUpdate, entityDto);
            }
        }

        /// <summary>
        /// Soft-deletes the entity.
        /// </summary>
        /// <param name="entity">Entity to be soft-deleted.</param>
        public async Task<Result<TDto>> DeleteAsync(TDto entityDto)
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
                entity.ModifiedDate = DateTime.Now;

                _dbContext.Entry(entity).State = EntityState.Modified;

                await Save();

                return new Result<TDto>(entityDto);
            }
            catch (Exception ex)
            {
                return new Result<TDto>(ex, ExceptionLocations.SqlRepositoryDelete, entityDto);
            }
        }

        /// <summary>
        /// Wrapper for DbContex.SaveCahngesAsync() method.
        /// </summary>
        private async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_dbContext != null)
                    {
                        _dbContext.Dispose();
                        _dbContext = null;
                    }
                }

                disposed = true;
            }
        }
    }
}
