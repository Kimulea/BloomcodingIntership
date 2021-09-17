using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bloomcoding.Common.Models.PagedRequest;
using Bloomcoding.Dal.Extensions;
using Bloomcoding.Dal.Interfaces;
using Bloomcoding.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bloomcoding.Dal.Repositories
{
    public class EFCoreRepository : IRepository
    {
        private readonly BloomcodingDbContext _bloomcodingDbContext;
        private readonly IMapper _mapper;

        public EFCoreRepository(BloomcodingDbContext bloomcodingDbContext, IMapper mapper)
        {
            _bloomcodingDbContext = bloomcodingDbContext;
            _mapper = mapper;
        }

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return await _bloomcodingDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById<TEntity>(int id) where TEntity : BaseEntity
        {
            return await _bloomcodingDbContext.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> GetByIdWithInclude<TEntity>(int id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<TEntity> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return await _bloomcodingDbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await _bloomcodingDbContext.SaveChangesAsync();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _bloomcodingDbContext
                .Set<TEntity>()
                .Add(entity);
        }

        public async Task<TEntity> Delete<TEntity>(int id) where TEntity : BaseEntity
        {
            var entity = await _bloomcodingDbContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"Object of type {typeof(TEntity)} with id { id } not found");
            }

            _bloomcodingDbContext.Set<TEntity>().Remove(entity);

            return entity;
        }

        public async Task<PagedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : BaseEntity
                                                                                                    where TDto : class
        {
            return await _bloomcodingDbContext.Set<TEntity>().CreatePaginatedResultAsync<TEntity, TDto>(pagedRequest, _mapper);
        }

        private IQueryable<TEntity> IncludeProperties<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
        {
            IQueryable<TEntity> entities = _bloomcodingDbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
    }
}
