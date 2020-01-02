using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Slipways.Data.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> InsertRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> SelectAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> SelectByConditionAsync(Func<T, bool> expression, CancellationToken cancellationToken = default);
        Task<T> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
