
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

// namespace biletmajster_backend.Domain;
namespace biletmajster_backend.Domain;
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
    public string? PlaceSchema { get; set; }
    public EventStatus Status { get; set; }
    public ICollection<Category> Categories { get; set; }
    public long FreePlace { get; set; }
    public long MaxPlace { get; set; }
    public ICollection<Place> Places { get; set; }
    public List<Reservation> Reservations { get; set; }
    public Organizer Organizer { get; set; }
    public List<EventPhotos> EventPhotos { get; set; }
    public ModelEvent()
    {
        Categories = new List<Category>();
        Places = new List<Place>();
        Reservations = new List<Reservation>();
        EventPhotos = new List<EventPhotos>();
    }
    public void UpdateData(ModelEvent e)
    {
        MaxPlace += e.FreePlace - FreePlace;
        FreePlace = e.FreePlace;
        StartTime = e.StartTime;
        Title = e.Title;
        Name = e.Name;
        EndTime = e.EndTime;
        PlaceSchema = e.PlaceSchema;
    }
    public List<Place> GetFreePlaces()
    {
        return Places.Where(x => x.Free.Equals(true)).ToList();
    }
    public bool UpdateStatus()
    {
        if (this.Status == EventStatus.Cancelled)
            return false;
        var currtime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
        if (EndTime < currtime)
        {
            this.Status = EventStatus.Done;
            return true;
        }
        if(StartTime < currtime)
        {
            this.Status = EventStatus.Pending;
            return true;
        }
        return false;
    }
}