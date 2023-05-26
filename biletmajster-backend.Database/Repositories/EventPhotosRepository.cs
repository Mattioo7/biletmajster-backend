using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace biletmajster_backend.Database.Repositories
{
    public class EventPhotosRepository : BaseRepository<EventPhotos>, IEventPhotosRepository
    {
        protected override DbSet<EventPhotos> DbSet => MDbContext.EventsPhotos;

        public EventPhotosRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddPhotoAsync(EventPhotos photo)
        {
            var result = await DbSet.AddAsync(photo);
            return await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            var saved = await MDbContext.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<EventPhotos> GetPhotoByLink(string Link)
        {
            var list = await DbSet.FirstOrDefaultAsync((x)=>x.DownloadLink== Link);
            return list;
        }

        public async Task<bool> DeletePhoto(EventPhotos photo)
        {
            DbSet.Remove(photo);
            return await SaveChangesAsync();
        }
    }
}
