using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : IEntityGuid
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task LogicDelete(int id);
        Task SaveChangesAsync();
        Task<T> GetByIdAsync(int id, string include = null);
        IQueryable<T> Get();
    }
}


