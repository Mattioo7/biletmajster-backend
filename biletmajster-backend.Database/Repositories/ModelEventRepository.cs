using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database.Repositories
{
    public class ModelEventRepository : BaseRepository<ModelEvent>, IModelEventRepository
    {
        protected override DbSet<ModelEvent> DbSet => mDbContext.ModelEvents;
        public ModelEventRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        //Interface Implementation
        public async Task<bool> AddEvent(ModelEvent _event)
        {
            await mDbContext.Places.AddRangeAsync(_event.Places);
            // TODO: Use ICategoryInterface to update!

            mDbContext.Categories.UpdateRange(_event.Categories);
            mDbContext.Organizers.Update(_event.Organizer);

            await DbSet.AddAsync(_event);
            return await SaveChanges();
        }
        public async Task<bool> SaveChanges()
        {
            var saved = await mDbContext.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<ModelEvent> GetEventById(long id)
        {
            return await DbSet.Include(c => c.Categories).Include(p => p.Places).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ModelEvent>> GetAllEvents()
        {
            return await DbSet.Include(c => c.Categories).Include(p => p.Places).ToListAsync();
        }

        public async Task<bool> DeleteEvent(long id)
        {
            var Event = await GetEventById(id);
            if (Event != null)
                DbSet.Remove(Event);
            else
                return false;
            return await SaveChanges();
        }

        public async Task<bool> PatchEvent(ModelEvent body, List<Place> place)
        {
            if (place.Count > 0)
                await mDbContext.Places.AddRangeAsync(place);

            mDbContext.Categories.UpdateRange(body.Categories);
            DbSet.Update(body);
            return await SaveChanges();
        }
    }
}
