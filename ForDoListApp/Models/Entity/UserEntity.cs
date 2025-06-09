using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Entity;

public class UserEntity
{
    [Key]
    public int UserId { get; set; }

    public UserEntity()
    {
        UserTasks = new List<TaskEntity>();
    }

    [Required, MaxLength(50)]
    public string UserName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TaskEntity> UserTasks { get; set; }
}
