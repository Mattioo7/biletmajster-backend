using biletmajster_backend.Domain;
using biletmajster_backend.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database.Repositories
{
    public class ModelEventRepository : BaseRepository<ModelEvent>, IModelEventRepository
    {
        protected override DbSet<ModelEvent> DbSet => MDbContext.ModelEvents;

        public ModelEventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        //Interface Implementation
        public async Task<bool> AddEventAsync(ModelEvent @event)
        {
            await MDbContext.Places.AddRangeAsync(@event.Places);
            // TODO: Use ICategoryInterface to update!
            MDbContext.Categories.UpdateRange(@event.Categories);

            MDbContext.Categories.UpdateRange(@event.Categories);
            MDbContext.Organizers.Update(@event.Organizer);

            await DbSet.AddAsync(@event);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var saved = await MDbContext.SaveChangesAsync();
            return saved > 0;
        }

        public Task<List<ModelEvent>> GetEventsByOrganizerIdAsync(long organizerId)
        {
            return DbSet.Where(x => x.Organizer.Id == organizerId).ToListAsync();
        }

        public Task<List<ModelEvent>> GetEventsByCategoryAsync(long categoryId)
        {
            return DbSet.Where(x => x.Categories.Any(c => c.Id == categoryId)).ToListAsync();
        }

        public async Task<ModelEvent> GetEventByIdAsync(long id)
        {
            return await DbSet.Include(c => c.Categories).Include(p => p.Places).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ModelEvent>> GetAllEventsAsync()
        {
            return await DbSet.Include(c => c.Categories).Include(p => p.Places).ToListAsync();
        }

        public async Task<bool> PatchEventAsync(ModelEvent body, List<Place> place)
        {
            if (place.Count > 0)
                await MDbContext.Places.AddRangeAsync(place);

            MDbContext.Categories.UpdateRange(body.Categories);
            DbSet.Update(body);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteEventAsync(long id)
        {
            var @event = await GetEventByIdAsync(id);
            if (@event != null)
                DbSet.Remove(@event);
            else
                return false;
            return await SaveChangesAsync();
        }


        public async Task<bool> ReservePlaceAsync(ModelEvent modelEvent, long? placeId)
        {
            Place place;
            try
            {
                place = modelEvent.Places.First(p => p.Id == placeId);
            }
            catch (Exception)
            {
                throw new Exception("Place not found!");
            }

            if (!place.Free)
            {
                throw new Exception("Place is already reserved!");
            }

            modelEvent.FreePlace--;
            place.Free = false;

            DbSet.Update(modelEvent);

            return await SaveChangesAsync();
        }

        public async Task<long> ReserveRandomPlace(ModelEvent modelEvent)
        {
            var place = modelEvent.Places.First(p => p.Free);

            if (place == null)
            {
                throw new Exception("No free places!");
            }

            place.Free = false;
            modelEvent.FreePlace--;
            DbSet.Update(modelEvent);
            await SaveChangesAsync();
            return place.Id;
        }

        public Task DeleteReservationAsync(ModelEvent reservationEvent, Place reservationPlace)
        {
            reservationPlace.Free = true;
            reservationEvent.FreePlace++;
            DbSet.Update(reservationEvent);
            return SaveChangesAsync();
        }
    }
}