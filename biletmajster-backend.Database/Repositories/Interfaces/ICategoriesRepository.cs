using biletmajster_backend.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        public Task<Category> GetCategoryById(int id);
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryByName(string name);
        public Task<bool> AddCategory(Category category);
        public Task<bool> SaveChanges();
    }
}
