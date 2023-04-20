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

        public async Task<ModelEvent> GetEventById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<ModelEvent>> GetAllEvents()
        {
            return await DbSet.ToListAsync();
        }
    }
}
