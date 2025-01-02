using System.Linq.Expressions;

namespace Clothing.CMS.EntityFrameworkCore.Pattern.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        T Add(T t);
        Task<T> AddAsync(T t);
        Task<ICollection<T>> AddRangeAsync(ICollection<T> entities);
		int Count();
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        Task<int> DeleteAsync(T entity);
        void Dispose();
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsyncIncluding(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(int id);
        void Save();
        Task<int> SaveAsync();
        T Update(T t, object key);
        Task<T> UpdateAsync(T t, object key);
        Task<T> UpdateAsync(T t, object key, params Expression<Func<T, object>>[] includeProperties);
    }
}
