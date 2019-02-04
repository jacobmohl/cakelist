using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cakelist.Business.Interfaces
{
    interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
