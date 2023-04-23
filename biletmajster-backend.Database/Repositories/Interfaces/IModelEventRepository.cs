using biletmajster_backend.Database.Entities;
using System.Threading.Tasks;

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
        Task<List<ModelEvent>> GetEventsByOrganizerIdAsync(long organizerId);
        Task<List<ModelEvent>> GetEventsByCategoryAsync(long categoryId);
    }
}
