using BSBookingQuery.Domain.Entities;
using BSBookingQuery.Domain.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BSBookingQuery.Infrastucture.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        string errorMessage = string.Empty;
        private readonly BSDbContext _context;
        protected DbSet<TEntity> _entities;
        public Repository(BSDbContext context)
        {
            _context = context;
        }

        #region Utilities
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _context.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }
        #endregion
        #region Methods
        public async virtual Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity == null)
                    return false;

                Entities.Remove(entity);
                _context.SaveChanges(); 
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity == null)
                    return null;

                else
                    return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async virtual Task<IQueryable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Task.FromResult(Entities.Where(where));
        }
        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            try
            {
                _context.Entry(entity).State = EntityState.Added;

            }
            catch (DbUpdateException exception)
            {
               
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }

        }
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual void Insert(IList<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            foreach (var item in entities)
                Insert(item);

        }
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            try
            {
                Entities.AddRange(entities);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("Source: {0} Error: {1}",
                                                ex.Source,
                                                ex.Message);
                throw ex;
            }

        }
     
        public virtual void Update(IList<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            try
            {
                foreach (var item in entities)
                    Update(item);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual void Update(IEnumerable<TEntity> entities)
        {

            Update(entities.ToList());
        }
       
        public virtual bool Delete(long id)
        {
            try
            {
                var entity = Entities.Find(id);
                if (entity == null)
                    return false;

                Entities.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual bool Delete(string id)
        {
            try
            {
                var entity = Entities.Find(id);
                if (entity == null)
                    return false;

                Entities.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual bool Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var entities = Entities.Where(where);
                Entities.RemoveRange(entities);
                return true;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual bool Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    return false;

                Entities.Remove(entity);
                return true;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual TEntity Get(int id)
        {
            try
            {
                return Entities.Find(id);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual TEntity Get(long id)
        {
            try
            {
                return Entities.Find(id);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual TEntity Get(string id)
        {
            try
            {
                return Entities.Find(id);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual TEntity Get(object id)
        {
            try
            {
                return Entities.Find(id);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return Entities.FirstOrDefault(where);
        }
        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return Entities.FirstOrDefaultAsync(where);
        }
        public virtual IQueryable<TEntity> GetAll()
        {
            try
            {
                return Entities;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
      
      
        public virtual void Save()
        {
            _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public virtual int Count()
        {
            return Entities.Count();

        }
        public virtual int Count(Expression<Func<TEntity, bool>> where)
        {
            return Entities.Count(where);

        }

        public IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            var result = Entities.FromSqlRaw(query, parameters);
            return result;
        }

        public Task<List<T>> ExecuteSqlQuery<T>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> SearchHotels(string location, int? minRating, int? maxRating, string name)
        {
            return _context.Set<TEntity>()
            .FromSqlInterpolated($@"
                EXEC SearchHotels
                @Location = {location},
                @MinRating = {minRating},
                @MaxRating = {maxRating},
                @Name = {name}")
            .ToList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

        

        #endregion
    }
}
