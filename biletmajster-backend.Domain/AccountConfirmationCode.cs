using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biletmajster_backend.Domain;

public class AccountConfirmationCode
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public Organizer Organizer { get; set; }
    public string Code { get; set; }
    public DateTime CreatedAt { get; set; }
}