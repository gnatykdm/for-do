using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class PriorityEntity
{
    [Key]
    public int PriorityId { get; set; }

    public PriorityEntity()
    {
        Tasks = new List<TaskEntity>();
    }

    [Required, MaxLength(20)]
    public string PriorityName { get; set; } = string.Empty;

    [Required]
    public int PriorityLevel { get; set; }

    public ICollection<TaskEntity> Tasks { get; set; }
}