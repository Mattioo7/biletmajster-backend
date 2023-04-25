using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext MDbContext;
        protected abstract DbSet<TEntity> DbSet { get; }
        
        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.MDbContext = dbContext;
        }
    }
}
