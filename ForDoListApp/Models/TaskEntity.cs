using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Enums;
using TaskColorEnum = Enums.TaskColor;
using TaskStatusEnum = Enums.TaskStatus;

namespace Model;

public class TaskEntity
{
    [Key]
    public int TaskId { get; set; }
    
    public TaskEntity()
    {
        TaskHistories = new List<TaskHistoryEntity>();
    }

    [Required]
    public int UserId { get; set; }

    public int? CategoryId { get; set; }

    public int? PriorityId { get; set; }

    [Required, MaxLength(100)]
    public string TaskTitle { get; set; } = string.Empty;

    public string? TaskDescription { get; set; }

    public TaskColor Color { get; set; } = TaskColor.Purple;

    public DateTime? DueDate { get; set; }

    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public UserEntity User { get; set; } = null!;

    public CategoryEntity? Category { get; set; }

    public PriorityEntity? Priority { get; set; }

    public ICollection<TaskHistoryEntity> TaskHistories { get; set; }
}
