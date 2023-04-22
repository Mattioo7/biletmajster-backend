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
        public async Task<bool> AddPlace(Place place)
        {
            await DbSet.AddAsync(place);
            return await SaveChanges();
        }
        public async Task<bool> RemovePlace(Place place)
        {
            DbSet.Remove(place);
            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            var saved = await mDbContext.SaveChangesAsync();
            return saved > 0;
        }
    }


}
