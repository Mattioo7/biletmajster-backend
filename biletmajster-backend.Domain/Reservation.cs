using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace biletmajster_backend.Domain;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public ModelEvent Event { get; set; }
    public Place Place { get; set; }
    public string ReservationToken { get; set; }
}