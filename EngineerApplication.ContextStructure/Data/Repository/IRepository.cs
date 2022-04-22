using System.Linq.Expressions;

namespace EngineerApplication.ContextStructure.Data.Repository
{
  public interface IRepository<T> where T : class
  {
    Task<T> GetAsync(int id);

    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeProperties = null
        );

    Task<T> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null
        );

    Task AddAsync(T entity);

    Task RemoveAsync(int id);
    void Remove(T entity);
  }
}
