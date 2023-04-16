using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories.Interfaces;

public interface IReservationRepository
{
    public Task<Reservation> AddReservationAsync(Reservation reservation);
    Task<Reservation> FindByReservationTokenAsync(string reservationToken);
    Task DeleteReservationAsync(Reservation reservation);
}