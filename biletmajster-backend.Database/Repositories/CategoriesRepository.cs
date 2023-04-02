using biletmajster_backend.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Category> DbSet => mDbContext.Categories;
    }
}