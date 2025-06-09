using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Entity.Enums;

namespace Models.Entity
{
    public class TaskEntity
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string TaskTitle { get; set; } = string.Empty;

        public string? TaskDescription { get; set; }

        public DateTime? DueDate { get; set; }

        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.PENDING;

        public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.LOW;

        public TaskCategoryEnum Category { get; set; } = TaskCategoryEnum.PERSONAL;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public UserEntity? User { get; set; } 
    }
}