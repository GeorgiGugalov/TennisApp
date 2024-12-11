using Microsoft.EntityFrameworkCore;
using TennisApp.Common.Repositories;

namespace TennisApp.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public Repository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dbSet = this.context.Set<TEntity>();
        }

        protected DbSet<TEntity> dbSet { get; set; }

        protected ApplicationDbContext context { get; set; }

        public virtual IQueryable<TEntity> All() => this.dbSet;

        public virtual IQueryable<TEntity> AllAsNoTracking() => this.dbSet.AsNoTracking();

        public virtual Task AddAsync(TEntity entity) => this.dbSet.AddAsync(entity).AsTask();

        public virtual void Update(TEntity entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity) => this.dbSet.Remove(entity);

        public Task<int> SaveChangesAsync() => this.context.SaveChangesAsync();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context?.Dispose();
            }
        }
    }
}
