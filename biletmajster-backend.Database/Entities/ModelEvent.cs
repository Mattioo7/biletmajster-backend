
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace biletmajster_backend.Database.Entities;

public class ModelEvent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Title { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Name { get; set; }
    public string PlaceSchema { get; set; }
    public EventStatus Status { get; set; }
    public ICollection<Category> Categories { get; set; }
    public long FreePlace { get; set; }
    public long MaxPlace { get; set; }
    public ICollection<Place> Places { get; set; }
    public ModelEvent()
    {
        this.Categories = new List<Category>();
        this.Places = new List<Place>();
    }
}