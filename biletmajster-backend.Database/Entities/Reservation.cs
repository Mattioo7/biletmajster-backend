namespace biletmajster_backend.Database.Entities;

public class Reservation
{
    public long Id { get; set; }
    public long EventId { get; set; }
    public long PlaceId { get; set; }
    public string ReservationToken { get; set; }
}