using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class CategoryEntity
{
    [Key]
    public int CategoryId { get; set; }

    public CategoryEntity()
    {
        Tasks = new List<TaskEntity>();
    }

    public int UserId { get; set; }

    [Required, MaxLength(50)]
    public string CategoryName { get; set; } = string.Empty;

    public UserEntity User { get; set; } = null!;

    public ICollection<TaskEntity> Tasks { get; set; }
}