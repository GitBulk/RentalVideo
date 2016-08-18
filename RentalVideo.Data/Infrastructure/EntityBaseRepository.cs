using RentalVideo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace RentalVideo.Data.Infrastructure
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private RentalVideoContext context;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected RentalVideoContext DbContext
        {
            get
            {
                return context ?? (context = this.DbFactory.Init());
            }
        }

        public EntityBaseRepository(IDbFactory factory)
        {
            this.DbFactory = factory;
        }

        public virtual IQueryable<T> All
        {
            get
            {
                return GetAll();
            }
        }

        public void Add(T entity)
        {
            //DbEntityEntry entry = this.DbContext.Entry<T>(entity);
            this.DbContext.Set<T>().Add(entity);
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = this.DbContext.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void Edit(T entity)
        {
            DbEntityEntry entry = this.DbContext.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.DbContext.Set<T>();
        }

        public T GetSingle(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
