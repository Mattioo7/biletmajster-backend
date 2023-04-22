using biletmajster_backend.Database.Entities;
using System.Threading.Tasks;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface IModelEventRepository
    {
        //GET:
        public Task<ModelEvent> GetEventById(long id);
        public Task<List<ModelEvent>> GetAllEvents();
        
        public Task<bool> DeleteEvent(long id);
        public Task<bool> PatchEvent(ModelEvent body, List<Place> place); 
        public Task<bool> AddEvent(ModelEvent _event);
        
        public Task<bool> SaveChanges();
    }
}
