
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
    public Organizer Organizer { get; set; }
    public ModelEvent()
    {
        this.Categories = new List<Category>();
        this.Places = new List<Place>();
    }
    public void UpdateData(ModelEvent e)
    {
        this.MaxPlace += e.FreePlace - this.FreePlace;
        this.FreePlace = e.FreePlace;
        this.StartTime = e.StartTime;
        this.Title = e.Title;
        this.Name = e.Name;
        this.EndTime = e.EndTime;
        this.PlaceSchema = e.PlaceSchema;
    }
    public List<Place> GetFreePlaces()
    {
        return Places.Where(x => x.Free.Equals(true)).ToList();
    }
}