using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
