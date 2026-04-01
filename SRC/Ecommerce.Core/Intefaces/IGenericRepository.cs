using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Intefaces
{
    public interface IGenericRepository<T>where T:class
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, Object>>[] includes);

        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(int id, params Expression<Func<T, Object>>[] includes);

        Task AddAsync(T Entity);

        Task UpdateAsync(T Entity);

        Task DeleteAsync(int id);

        Task<int> CountAsync();
    }
}
