using Common.Entities;
using Common.Entities.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task LogicDelete(int id);
        Task SaveChangesAsync();
        Task<T?> GetByIdAsync(int id, bool? isEnabled = null, string? include = null);
        Task<IEnumerable<T>> GetAllAsync(bool? isEnabled = null);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);


    }
}
