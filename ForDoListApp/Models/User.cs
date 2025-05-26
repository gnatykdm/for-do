using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class User
{
    public User()
    {
        Tasks = new List<Task>();
        Categories = new List<Category>();
        TaskHistories = new List<TaskHistory>();
    }

    public int UserId { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Task> Tasks { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<TaskHistory> TaskHistories { get; set; }
}
