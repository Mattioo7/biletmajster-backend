using biletmajster_backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biletmajster_backend.Database.Interfaces
{
    public interface IEventPhotosRepository
    {
        public Task<bool> AddPhotoAsync(EventPhotos photo);
        public Task<EventPhotos> GetPhotoByLink(string Link);
        public Task<bool> DeletePhoto(EventPhotos photo);
        public Task<bool> SaveChangesAsync();
    }
}
