using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class Priority
{
    public Priority()
    {
        Tasks = new List<Task>();
    }

    public int PriorityId { get; set; }

    [Required, MaxLength(20)]
    public string PriorityName { get; set; } = string.Empty;

    [Required]
    public int PriorityLevel { get; set; }

    public ICollection<Task> Tasks { get; set; }
}
