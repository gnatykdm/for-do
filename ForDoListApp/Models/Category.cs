using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class Category
{
    public Category()
    {
        Tasks = new List<Task>();
    }

    public int CategoryId { get; set; }

    public int UserId { get; set; }

    [Required, MaxLength(50)]
    public string CategoryName { get; set; } = string.Empty;

    public User User { get; set; } = null!;

    public ICollection<Task> Tasks { get; set; }
}
