using System;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class TaskHistoryEntity
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(255)]
        public string ChangeDescription { get; set; } = string.Empty;

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public TaskEntity Task { get; set; } = null!;
        public UserEntity User { get; set; } = null!;
    }
