using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shops.Api.Base
{
    public interface IBaseRepository<TDocument> where TDocument : Document
    {
        Task<int> CreateAsync(TDocument document);
        Task CreateManyAsync(IEnumerable<TDocument> documents);
        Task UpdateAsync(TDocument document);
        Task DeleteAsync(TDocument document);
        Task<TDocument> GetByIdAsync(int id);
        Task<IEnumerable<TDocument>> GetAllAsync();
        Task<IEnumerable<TDocument>> FilterAsync(Func<TDocument, bool> predicate);

        Task<IEnumerable<TDocument>> FilterWithIncludeAsync(Func<TDocument, bool> predicate,
            params Expression<Func<TDocument, object>>[] include);

        Task<TDocument> GetByIdWithIncludeAsync(int id, params Expression<Func<TDocument, object>>[] include);
    }
}