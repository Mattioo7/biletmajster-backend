using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database.Repositories
{
    public abstract class BaseRepository<Entity> where Entity : class
    {
        protected ApplicationDbContext mDbContext;
        protected abstract DbSet<Entity> DbSet { get; }
        
        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.mDbContext = dbContext;
        }
    }
}
