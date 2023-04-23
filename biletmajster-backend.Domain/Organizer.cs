using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace biletmajster_backend.Domain;

public class Organizer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; } 
    public ICollection<ModelEvent> Events { get; set; }
    public OrganizerAccountStatus Status { get; set; }
    public Organizer()
    {
        this.Events = new List<ModelEvent>();
    }
}