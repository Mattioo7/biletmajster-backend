using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace biletmajster_backend.Database.Entities;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long EventId { get; set; }
    public long PlaceId { get; set; }
    public string ReservationToken { get; set; }
}