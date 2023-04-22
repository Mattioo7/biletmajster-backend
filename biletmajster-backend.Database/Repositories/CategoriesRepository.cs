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

        public async Task<bool> AddCategory(Category category)
        {
            await DbSet.AddAsync(category);
            return await this.SaveChanges();
        }
        public async Task<Category> GetCategoryById(long id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Category> GetCategoryByName(string name)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<bool> SaveChanges()
        {
            var saved = await mDbContext.SaveChangesAsync();
            return saved > 0;
        }
        public async Task<bool> UpdateCategory(Category category)
        {
            DbSet.Update(category);
            return await this.SaveChanges();
        }
        public async Task<bool> UpdateCategories(List<Category> category)
        {
            DbSet.UpdateRange(category);
            return await this.SaveChanges();
        }
    }
}