using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface IModelEventRepository
    {
        //GET:
        public Task<ModelEvent> GetEventByIdAsync(long id);
        public Task<List<ModelEvent>> GetAllEventsAsync();
        public Task<bool> PatchEventAsync(ModelEvent body, List<Place> place);
        public Task<bool> DeleteEventAsync(long id);

        public Task<bool> AddEventAsync(ModelEvent _event);
        
        public Task<bool> SaveChangesAsync();
        public Task<bool> ReservePlaceAsync(ModelEvent modelEvent, long? placeId);
        public Task<long> ReserveRandomPlace(ModelEvent modelEvent);
        public Task DeleteReservationAsync(ModelEvent reservationEvent, Place reservationPlace);
        public Task<List<ModelEvent>> GetEventsByOrganizerIdAsync(long organizerId);
        public Task<List<ModelEvent>> GetEventsByCategoryAsync(long categoryId);
    }
}
