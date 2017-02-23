using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebAppCore.Data;

namespace WebAppCore.Infrastructure
{
     public class GenericRepository<TEntity> where TEntity : class
    {
        internal DbSet<TEntity> DbSet;

        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> All => DbSet.AsNoTracking().ToList();

        public IEnumerable<TEntity> AllInclude
        (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public IEnumerable<TEntity> FindByInclude
          (Expression<Func<TEntity, bool>> predicate,
          params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<TEntity> results = query.Where(predicate).ToList();
            return results;
        }

        private IQueryable<TEntity> GetAllIncluding
        (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = DbSet.AsNoTracking();

            return includeProperties.Aggregate
              (queryable, (current, includeProperty) => current.Include(includeProperty));
        }
        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {

            IEnumerable<TEntity> results = DbSet.AsNoTracking()
              .Where(predicate).ToList();
            return results;
        }

        public TEntity FindByKey(int id)
        {
            Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id);
            return DbSet.AsNoTracking().SingleOrDefault(lambda);
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = FindByKey(id);
            DbSet.Remove(entity);
            _context.SaveChanges();


        }
    }
}
