using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskColorEnum = Enums.TaskColor;
using TaskStatusEnum = Enums.TaskStatus;

namespace Model;

public class Task
{
    public Task()
    {
        TaskHistories = new List<TaskHistory>();
    }

    public Task(int taskId, int userId, string taskTitle, TaskColorEnum color, TaskStatusEnum status, DateTime createdAt, DateTime updatedAt, User user)
    {
        TaskId = taskId;
        UserId = userId;
        TaskTitle = taskTitle;
        Color = color;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        User = user;
        TaskHistories = new List<TaskHistory>();
    }

    public int TaskId { get; set; }

    [Required]
    public int UserId { get; set; }

    public int? CategoryId { get; set; }

    public int? PriorityId { get; set; }

    [Required, MaxLength(100)]
    public string TaskTitle { get; set; } = string.Empty;

    public string? TaskDescription { get; set; }

    public TaskColorEnum Color { get; set; } = TaskColorEnum.Purple;

    public DateTime? DueDate { get; set; }

    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User User { get; set; } = null!;

    public Category? Category { get; set; }

    public Priority? Priority { get; set; }

    public ICollection<TaskHistory> TaskHistories { get; set; }
}
