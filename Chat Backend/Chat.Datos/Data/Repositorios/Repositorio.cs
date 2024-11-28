using Chat.Datos.Data.Repositorios.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chat.Datos.Data.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly DbContext _context;
        internal DbSet<T> dbset;
        public Repositorio(DbContext context)
        {
            _context = context;
            this.dbset =  context.Set<T>();
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);           
        }

        public T GetById(int id)
        {
            return dbset.Find(id);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;

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

            if (orderBy !=null)
            {
                return orderBy(query).ToList();
            }

            return query.AsNoTracking().ToList();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;

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

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.AsNoTracking().ToListAsync();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query =query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.AsNoTracking().FirstOrDefault();
        }
        public async Task<T>GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;

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

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task Remove(T entity)
        {
             dbset.Remove(entity);
        }
    }
}
