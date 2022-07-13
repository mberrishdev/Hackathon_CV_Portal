﻿using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Data.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly CvPortalDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(CvPortalDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Table => _dbSet;

        public async Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbSet.ToListAsync();

            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();

        }

        public async Task<T> GetAsync(object key)
        {
            var obj = new object[1] { key };
            return await _dbSet.FindAsync(obj);
        }

        public async Task<T> GetForUpdateAsync(object key)
        {
            var obj = new object[1] { key };
            return await _dbSet.FindAsync(obj);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                return;

            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(params object[] key)
        {
            var entity = await GetAsync(key);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }
    }
}
