using Common.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repository
{
    public class MySqlRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly DataContext _context;
        public MySqlRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public Task Delete(T entity)
        {
            _context.Remove(entity);

            return Task.CompletedTask;
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id, bool? isEnabled = null, string? include = null)
        {
            var query = _context.Set<T>().AsQueryable().Where(e => !isEnabled.HasValue || e.IsEnabled == isEnabled.Value);
            var entity = await query.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return null;

            if (include != null)
            {
                var includeProperties = include.Split(',');

                foreach (var path in includeProperties)
                {
                    _context.Entry(entity).Reference(path.Trim()).Load();
                }
            }

            return entity;
        }

        public async Task LogicDelete(int id)
        {
            var entity = await GetByIdAsync(id);
            entity.IsEnabled = !entity.IsEnabled;
            await Update(entity);
        }

        public Task Update(T entity)
        {
            _context.Update(entity);
            entity.LastUpdate = DateTime.Now;
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool? isEnabled = null)
        {
            return await _context.Set<T>().Where(e => !isEnabled.HasValue || e.IsEnabled == isEnabled.Value).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
