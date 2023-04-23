using biletmajster_backend.Domain;

namespace biletmajster_backend.Database.Interfaces;

public interface IReservationRepository
{
    public Task<Reservation> AddReservationAsync(Reservation reservation);
    Task<Reservation> FindByReservationTokenAsync(string reservationToken);
    Task DeleteReservationAsync(Reservation reservation);
}