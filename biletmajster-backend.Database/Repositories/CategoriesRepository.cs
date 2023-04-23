using Microsoft.EntityFrameworkCore;
using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Interfaces;

namespace biletmajster_backend.Database.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Category> DbSet => mDbContext.Categories;

        public async Task<bool> AddCategoryAsync(Category category)
        {
            await DbSet.AddAsync(category);
            return await this.SaveChangesAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(long id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            var saved = await mDbContext.SaveChangesAsync();
            return saved > 0;
        }
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            DbSet.Update(category);
            return await this.SaveChangesAsync();
        }
        public async Task<bool> UpdateCategoriesAsync(List<Category> category)
        {
            DbSet.UpdateRange(category);
            return await this.SaveChangesAsync();
        }
    }
}