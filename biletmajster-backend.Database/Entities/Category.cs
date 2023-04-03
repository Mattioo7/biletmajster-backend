using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace biletmajster_backend.Database.Entities;

public class Category
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    public string Name { get; set; }
    public ICollection<ModelEvent> Events { get; set; }
    public Category() { 
        Events = new List<ModelEvent>();
    }
}