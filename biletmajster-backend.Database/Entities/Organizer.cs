namespace biletmajster_backend.Database.Entities;

public class Organizer
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<ModelEvent> Events { get; set; }
}