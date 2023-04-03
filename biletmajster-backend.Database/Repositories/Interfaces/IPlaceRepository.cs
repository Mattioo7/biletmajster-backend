using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface IPlaceRepository
    {
        public Task<bool> AddPlace(Place place);
        public Task<bool> SaveChanges();
    }
}
