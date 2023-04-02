
namespace biletmajster_backend.Database.Entities;

public class ModelEvent
{
    public long Id { get; set; }
    public string Title { get; set; }
    public long StartTime { get; set; }
    public long EndTime { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Name { get; set; }
    public string PlaceSchema { get; set; }
    public EventStatus Status { get; set; }
    public List<Category> Categories { get; set; }
    public long FreePlace { get; set; }
    public long MaxPlace { get; set; }
    public List<Place> Places { get; set; }
}