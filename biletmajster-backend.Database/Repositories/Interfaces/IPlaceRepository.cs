using biletmajster_backend.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biletmajster_backend.Database.Repositories.Interfaces
{
    public interface IPlaceRepository
    {
        public Task<bool> AddPlace(Place place);
        public Task<bool> SaveChanges();
    }
}
