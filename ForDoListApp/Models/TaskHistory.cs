using System;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class TaskHistory
{
    public int HistoryId { get; set; }

    [Required]
    public int TaskId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required, MaxLength(255)]
    public string ChangeDescription { get; set; } = string.Empty;

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public Task Task { get; set; } = null!;
    public User User { get; set; } = null!;
}
