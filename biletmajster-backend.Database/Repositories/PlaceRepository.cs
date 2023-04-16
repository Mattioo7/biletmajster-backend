using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database.Repositories
{
    public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
    {
        public PlaceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Place> DbSet => mDbContext.Places;

        public async Task<bool> AddPlaceAsync(Place place)
        {
            await DbSet.AddAsync(place);
            return await SaveChangesAsync();
        }

        public async Task<bool> RemovePlaceAsync(Place place)
        {
            DbSet.Remove(place);
            return await SaveChangesAsync();
        }

        public async Task<Place?> GetPlaceByIdAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            var saved = await mDbContext.SaveChangesAsync();
            return saved > 0;
        }
    }
}