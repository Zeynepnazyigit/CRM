using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Generics
{
    // Asenkron kodlama, yapmak istediğimiz işlemleri paralel olarak çalıştırmamıza olanak tanır, bu sayede bu işlemler yapılırken diğer işlemler etkilenmez.
    // Task -> void method olarak çalışır
    // Task<T> -> T tipinde değer döndüren method olarak çalışır ,return type metoudur.

    public interface IRepository<TEntity> where TEntity : class
    {
        // CRUD İşlemleri:
        Task CreateAsync(TEntity entity);
        Task CreateManyAsync(IEnumerable<TEntity> entities);

        Task<TEntity?> FindByIdAsync(object entityKey);
        Task<TEntity?> FindFirstAsync(Expression<Func<TEntity, bool>>? expression = null);
        Task<IQueryable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes);
        Task UpdateAsync(TEntity entity);
        Task UpdateManyAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteManyAsync(IEnumerable<TEntity> entities); 
        
        // Kontrol ve Sayma İşlemleri
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null);
        
    }

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _set;


        protected Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {

            // await: Asenkron metotların kullanıldığı sıraya alma ön ekidir . Bu sayede metot işlem sırasına dahil edilir.

            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task CreateManyAsync(IEnumerable<TEntity> entities)
        {
            await _set.AddRangeAsync(entities);
        }

        public async Task<TEntity?> FindByIdAsync(object entityKey)
        {
            return await _set.FindAsync(entityKey);
        }

        public async Task<TEntity?> FindFirstAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression != null ? await _set.FirstOrDefaultAsync(expression) : await _set.FirstOrDefaultAsync();

        }

        public async Task<IQueryable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes)
        {
            IQueryable<TEntity> data = expression != null ? _set.Where(expression) : _set;

            foreach (var include in includes)
            {
                data = data.Include(include);
            }

            // 
            return await Task.Run(() => data);
        }
        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _set.Update(entity);
                
            });
        }
        public async Task UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() =>
            {
                _set.UpdateRange(entities);
                
            });
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _set.Remove(entity);
                
            });
        }
        public async Task DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() =>
            {
                _set.RemoveRange(entities);
                
            });
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression != null ? await _set.AnyAsync(expression) : await _set.AnyAsync();
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression != null ? await _set.CountAsync(expression) : await _set.CountAsync();
        }
    }
}