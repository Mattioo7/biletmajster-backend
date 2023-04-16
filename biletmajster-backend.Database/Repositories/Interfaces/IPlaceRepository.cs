using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface IPlaceRepository
    {
        public Task<bool> AddPlaceAsync(Place place);
        public Task<bool> RemovePlaceAsync(Place place);
        public Task<Place?> GetPlaceByIdAsync(long id);
        public Task<bool> SaveChangesAsync();
    }
}
