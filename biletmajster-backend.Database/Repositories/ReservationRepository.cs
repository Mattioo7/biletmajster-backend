using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database.Repositories;

public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Reservation> DbSet => mDbContext.Reservations;
    public async Task<Reservation> AddReservationAsync(Reservation reservation)
    {
        var entry = await DbSet.AddAsync(reservation);

        await SaveChangesAsync();
        
        return entry.Entity;
    }

    public Task<Reservation> FindByReservationTokenAsync(string reservationToken)
    {
        return DbSet.FirstOrDefaultAsync(r => r.ReservationToken == reservationToken);
    }

    public async Task DeleteReservationAsync(Reservation reservation)
    {
        DbSet.Remove(reservation);
    }

    public async Task<bool> SaveChangesAsync()
    {
        var saved = await mDbContext.SaveChangesAsync();
        return saved > 0;
    }
}