﻿using biletmajster_backend.Domain;
using Microsoft.Extensions.Logging;

namespace biletmajster_backend.Database.Interfaces
{
    public interface IModelEventRepository
    {
        //GET:
        public Task<ModelEvent?> GetEventByIdAsync(long id);
        public Task<List<ModelEvent>> GetAllEventsAsync();
        public Task<bool> PatchEventAsync(ModelEvent body, List<Place> place);
        public Task<bool> CancelEventAsync(long id);
        public Task<bool> AddEventAsync(ModelEvent @event);
        public Task<bool> SaveChangesAsync();
        public Task<bool> ReservePlaceAsync(ModelEvent modelEvent, long? placeId);
        public Task<long> ReserveRandomPlace(ModelEvent modelEvent);
        public Task DeleteReservationAsync(ModelEvent reservationEvent, Place reservationPlace);
        public Task<List<ModelEvent>> GetEventsByOrganizerIdAsync(long organizerId);
        public Task<List<ModelEvent>> GetEventsByCategoryAsync(long categoryId);
        public Task<bool> UpdateEvent(ModelEvent @event);
        public Task<bool> UpdateEventStatus(ILogger logger);
    }
}
