using biletmajster_backend.Domain;
using biletmajster_backend.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database.Repositories;

public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Reservation> DbSet => MDbContext.Reservations;

    public async Task<Reservation> AddReservationAsync(Reservation reservation)
    {
        var entry = await DbSet.AddAsync(reservation);

        await SaveChangesAsync();

        return entry.Entity;
    }

    public Task<Reservation> FindByReservationTokenAsync(string reservationToken)
    {
        return DbSet.Include(r => r.Event)
            .Include(r => r.Place)
            .FirstOrDefaultAsync(r => r.ReservationToken == reservationToken);
    }

    public async Task DeleteReservationAsync(Reservation reservation)
    {
        DbSet.Remove(reservation);
    }

    public async Task<bool> SaveChangesAsync()
    {
        var saved = await MDbContext.SaveChangesAsync();
        return saved > 0;
    }
}