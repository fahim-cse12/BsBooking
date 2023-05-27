using BSBookingQuery.Domain.Entities;
using System.Linq.Expressions;

namespace BSBookingQuery.Domain.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> where);
        void Insert(T entity);
        IEnumerable<T> SearchHotels(string location, int? minRating, int? maxRating, string name);
        Task<List<T>> ExecuteSqlQuery<T>(string sql, params object[] parameters);
        void Insert(IList<T> entities);
        void Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IList<T> entities);
        void Update(IEnumerable<T> entities);

        bool Delete(T entity);
        bool Delete(long id);
        bool Delete(string id);
        bool Delete(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();

        T Get(int id);
        T Get(long id);
        T Get(string id);
        T Get(object id);
      
        T Get(Expression<Func<T, bool>> where);
      
        int Count(Expression<Func<T, bool>> where);
        int Count();


        void Save();
        Task<int> SaveAsync();

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}
