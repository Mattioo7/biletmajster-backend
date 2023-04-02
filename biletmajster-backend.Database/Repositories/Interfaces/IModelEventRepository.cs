using biletmajster_backend.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface IModelEventRepository
    {
        //GET:
        public ModelEvent GetEventById(int id);
        public List<ModelEvent> GetAllEvents();


        public bool AddEvent(ModelEvent _event);
        
        public bool SaveChanges();
    }
}
