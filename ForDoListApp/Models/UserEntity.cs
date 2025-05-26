using System;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class UserEntity
{
    [Key]
    public int UserId { get; set; }

    public UserEntity()
    {
        UserTasks = new List<TaskEntity>();
        Categories = new List<CategoryEntity>();
        TaskHistories = new List<TaskHistoryEntity>();
    }

    [Required, MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TaskEntity> UserTasks { get; set; }
    public ICollection<CategoryEntity> Categories { get; set; }
    public ICollection<TaskHistoryEntity> TaskHistories { get; set; }
}
