using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface IModelEventRepository
    {
        //GET:
        public Task<ModelEvent> GetEventById(long id);
        public Task<List<ModelEvent>> GetAllEvents();

        public Task<bool> DeleteEvent(long id);
        public Task<bool> PatchEvent(ModelEvent body); 
        public Task<bool> AddEvent(ModelEvent _event);
        
        public Task<bool> SaveChanges();
    }
}
