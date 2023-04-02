using biletmajster_backend.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } 
        public DbSet<ModelEvent> ModelEvents { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<ModelEvent>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }
    }
}