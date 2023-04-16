using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Interfaces;

public interface IReservationService
{
    public Task<Reservation> MakeReservationAsync(ModelEvent modelEvent, long? placeId);
    public Task DeleteReservationAsync(string reservationToken);
}