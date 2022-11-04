#nullable disable
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {
    protected readonly DbContext Context;
    internal DbSet<T> dbSet;

    public Repository(DbContext context)
    {
      Context = context;
      dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
      await dbSet.AddAsync(entity);
    }

    public async Task<T> GetAsync(int id)
    {
      var result = await dbSet.FindAsync(id);
      return result;
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
    {
      IQueryable<T> query = dbSet;

      if (filter != null)
      {
        query = query.Where(filter);
      }
      if (includeProperties != null)
      {
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
          StringSplitOptions.RemoveEmptyEntries))
        {
          query = query.Include(includeProperty);
        }
      }

      if (orderBy != null)
      {
        return await orderBy(query).ToListAsync();
      }
      return await query.ToListAsync();
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
    {
      IQueryable<T> query = dbSet;

      if (filter != null)
      {
        query = query.Where(filter);
      }

      if (includeProperties != null)
      {
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
          query = query.Include(includeProperty);
        }
      }

      return await query.FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(int id)
    {
      T entityToRemove = await dbSet.FindAsync(id);
      Remove(entityToRemove);
    }

    public void Remove(T entity)
    {
      dbSet.Remove(entity);
    }
  }
}
