using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisApp.Common.Repositories;
using TennisApp.Data;

namespace TennisApp.Web.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(ApplicationDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._dbSet = this._context.Set<T>();
        }
        protected DbSet<T> _dbSet { get; set; }
        protected ApplicationDbContext _context { get; set; }


        public virtual IQueryable<T> All() => this._dbSet;

        public virtual IQueryable<T> AllAsNoTracking() => this._dbSet.AsNoTracking();

        public virtual Task AddAsync(T entity) => this._dbSet.AddAsync(entity).AsTask();
        
        public virtual void Delete(T entity) => this._dbSet.Remove(entity);

        public Task<int> SaveChangesAsync() => this._context.SaveChangesAsync();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._context?.Dispose();
            }
        }

        public void Update(T entity)
        {
            var entry = this._context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
