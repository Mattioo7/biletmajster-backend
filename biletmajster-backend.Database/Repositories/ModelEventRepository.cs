using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biletmajster_backend.Database.Repositories
{
    public class ModelEventRepository : BaseRepository<ModelEvent>, IModelEventRepository
    {
        protected override DbSet<ModelEvent> DbSet => mDbContext.ModelEvents;
        public ModelEventRepository(ApplicationDbContext dbContext) : base(dbContext){ }

        //Interface Implementation
        public bool AddEvent(ModelEvent _event)
        {
            DbSet.Add(_event);
            return this.SaveChanges();
        }
        public bool SaveChanges()
        {
            var saved = mDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ModelEvent GetEventById(int id)
        {
            var ret = DbSet.FindAsync(id).Result;
            return ret;
        }

        public List<ModelEvent> GetAllEvents()
        {
            return DbSet.ToList();
        }
    }
}
