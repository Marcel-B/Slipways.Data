using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<T> InsertAsync(T entity);
        Task<int> InsertRangeAsync(IEnumerable<T> entity);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> SelectAllAsync();
        Task<IEnumerable<T>> SelectByConditionAsync(Func<T, bool> expression);
        Task<T> SelectByIdAsync(Guid id);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(Guid id);
    }
}