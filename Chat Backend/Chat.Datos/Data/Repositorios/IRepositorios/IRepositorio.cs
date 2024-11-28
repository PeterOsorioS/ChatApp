using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Datos.Data.Repositorios.IRepositorios
{
    public interface IRepositorio<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        Task AddAsync(T entity);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null
        );
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null
        );
        T GetFirstOrDefault(
           Expression<Func<T, bool>>? filter = null,
           string? includeProperties = null
        );
        Task<T> GetFirstOrDefaultAsync(
           Expression<Func<T, bool>>? filter = null,
           string? includeProperties = null
        );
        Task Remove(T entity);
    }
}
