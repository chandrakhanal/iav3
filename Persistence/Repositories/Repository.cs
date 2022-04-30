using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IndianArmyWeb.Core.IRepositories;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace IndianArmyWeb.Persistence.Repositories
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> dbSet;
        public Repository(DbContext context)
        {
            Context = context;
            dbSet = context.Set<TEntity>();
        }

        #region Get
        public async Task<TEntity> GetByIdAsync(int id) => await dbSet.FindAsync(id);
        public TEntity GetById(int id) => dbSet.Find(id);
        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return await query.ToListAsync();
        }
        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.ToList();
        }
        #endregion

        #region CUD
        public void Add(TEntity entity) => dbSet.Add(entity);
        
        public void AddRange(IEnumerable<TEntity> entities) => dbSet.AddRange(entities);
        
        public void Update(TEntity entity) => Context.Entry(entity).State = EntityState.Modified;
        
        public void Remove(TEntity entity) => dbSet.Remove(entity);
        
        public void RemoveRange(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);

        //public void AddOrUpdate(IEnumerable<TEntity> entities)
        //{
        //    DbSet.
        //}
        #endregion

        #region Fetch
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.Where(predicate);
            foreach (var include in includes)
                query = query.Include(include);
            return await query.ToListAsync();
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.Where(predicate);
            foreach (var include in includes)
                query = query.Include(include);
            return query.ToList();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
                query = dbSet.Include(include);
            return await query.SingleOrDefaultAsync(predicate);
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return await query.FirstOrDefaultAsync(predicate);
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.FirstOrDefault(predicate);
        }
        #endregion

        #region GetQuery
        public IQueryable<TEntity> GetAllAsQuery() => dbSet.AsNoTracking().AsQueryable<TEntity>();
        public IQueryable<TEntity> FindAsQuery(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.Where(predicate).AsNoTracking();
            foreach (var include in includes)
                query = query.Include(include);
            return query.AsQueryable();
        }
        #endregion

        #region Save
        public void Save() => Context.SaveChanges();
        public async Task SaveAsync() => await Context.SaveChangesAsync();
        #endregion
    }
}