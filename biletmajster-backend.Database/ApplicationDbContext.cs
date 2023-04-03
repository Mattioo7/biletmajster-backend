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
        public DbSet<Place> Places { get; set; }
        public DbSet<AccountConfirmationCode> AccountConfirmationCodes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}