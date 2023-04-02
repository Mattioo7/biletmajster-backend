using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace biletmajster_backend.Database.Entities;

public class Place
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long SeatNumber { get; set; }
    public bool Free { get; set; }
}