using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface IModelEventRepository
    {
        //GET:
        public Task<ModelEvent> GetEventById(int id);
        public Task<List<ModelEvent>> GetAllEvents();


        public Task<bool> AddEvent(ModelEvent _event);
        
        public Task<bool> SaveChanges();
    }
}
