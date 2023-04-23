using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace biletmajster_backend.Domain;

public class Place
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public long SeatNumber { get; set; }
    public bool Free { get; set; }
    public ModelEvent Event { get; set; }
    public Place() { }
    public Place(ModelEvent modelEvent, long SeatNr)
    {
        this.Event = modelEvent;
        SeatNumber = SeatNr;
    }
}