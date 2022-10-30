using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shops.Api.Base.DataAccess
{
    public class BaseRepository<TDocument> : IBaseRepository<TDocument> where TDocument : Document
    {
        private ApplicationContext _dbContext;
        private DbSet<TDocument> _dbSet;

        public BaseRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TDocument>();
        }

        public async Task<int> CreateAsync(TDocument document)
        {
            var result = await _dbSet.AddAsync(document);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task CreateManyAsync(IEnumerable<TDocument> documents)
        {
            await _dbSet.AddRangeAsync(documents);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TDocument document)
        {
            _dbContext.Entry(document).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TDocument document)
        {
            _dbSet.Remove(document);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TDocument> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TDocument>> FilterAsync(Func<TDocument, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Where(predicate));
        }
        
        public async Task<IEnumerable<TDocument>> FilterWithIncludeAsync(Func<TDocument, bool> predicate, params Expression<Func<TDocument, object>>[] include)
        {
            return await Task.Run(() => Include(include).Where(predicate)); 
        }

        public Task<TDocument> GetByIdWithIncludeAsync(int id, params Expression<Func<TDocument, object>>[] include)
        {
           return Include(include).FirstOrDefaultAsync(x => x.Id == id); 
        }

        private IQueryable<TDocument> Include(params Expression<Func<TDocument, object>>[] includeProperties)
        {
            var query = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => 
                current.Include(includeProperty));
        }
    }
}