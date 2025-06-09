using Models.Entity;
using System.Collections.Generic;

namespace ForDoListApp.Models
{
    public class TasksViewModel
    {
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
        public TaskEntity NewTask { get; set; } = new TaskEntity();
        public string? FilterStatus { get; set; }
        public string? SortBy { get; set; }
    }
}