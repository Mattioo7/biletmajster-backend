using biletmajster_backend.Domain;

namespace biletmajster_backend.Database.Interfaces
{
    public interface ICategoriesRepository
    {
        public Task<Category?> GetCategoryByIdAsync(long id);
        public Task<List<Category>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryByNameAsync(string name);
        public Task<bool> AddCategoryAsync(Category category);
        public Task<bool> UpdateCategoryAsync(Category category);
        public Task<bool> UpdateCategoriesAsync(List<Category> category);
        public Task<bool> SaveChangesAsync();
    }
}
