using biletmajster_backend.Domain;

namespace biletmajster_backend.Database.Interfaces
{
    public interface IPlaceRepository
    {
        public Task<bool> AddPlaceAsync(Place place);
        public Task<bool> RemovePlaceAsync(Place place);
        public Task<Place?> GetPlaceByIdAsync(long id);
        public Task<bool> SaveChangesAsync();
    }
}
