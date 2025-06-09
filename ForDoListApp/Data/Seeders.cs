using ForDoListApp.Data;
using Models.Entity;
using Models.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForDoListApp.Data
{
    public static class UserSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<UserEntity>
                {
                    new UserEntity { UserId = 1, UserName = "alice", Email = "alice@example.com" },
                    new UserEntity { UserId = 2, UserName = "bob", Email = "bob@example.com" },
                    new UserEntity { UserId = 3, UserName = "charlie", Email = "charlie@example.com" },
                    new UserEntity { UserId = 4, UserName = "diana", Email = "diana@example.com" },
                    new UserEntity { UserId = 5, UserName = "eve", Email = "eve@example.com" }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }

    public static class TaskSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Tasks.Any())
            {
                var tasks = new List<TaskEntity>
                {
                    new TaskEntity
                    {
                        TaskId = 1,
                        UserId = 1,
                        TaskTitle = "Finish EdemAI MVP",
                        TaskDescription = "Complete the MVP for the EdemAI project",
                        Status = TaskStatusEnum.PENDING,
                        Priority = TaskPriorityEnum.HIGH,
                        Category = TaskCategoryEnum.WORK,
                        CreatedAt = DateTime.UtcNow.AddDays(-3),
                        UpdatedAt = DateTime.UtcNow.AddDays(-1)
                    },
                    new TaskEntity
                    {
                        TaskId = 2,
                        UserId = 2,
                        TaskTitle = "Buy groceries",
                        TaskDescription = "Milk, Bread, Eggs",
                        Status = TaskStatusEnum.COMPLETED,
                        Priority = TaskPriorityEnum.LOW,
                        Category = TaskCategoryEnum.PERSONAL,
                        CreatedAt = DateTime.UtcNow.AddDays(-5),
                        UpdatedAt = DateTime.UtcNow.AddDays(-2)
                    },
                    new TaskEntity
                    {
                        TaskId = 3,
                        UserId = 3,
                        TaskTitle = "Prepare presentation",
                        TaskDescription = "Prepare slides for Monday's meeting",
                        Status = TaskStatusEnum.IN_PROGRESS,
                        Priority = TaskPriorityEnum.MEDIUM,
                        Category = TaskCategoryEnum.WORK,
                        CreatedAt = DateTime.UtcNow.AddDays(-4),
                        UpdatedAt = DateTime.UtcNow.AddDays(-1)
                    },
                    new TaskEntity
                    {
                        TaskId = 4,
                        UserId = 4,
                        TaskTitle = "Doctor appointment",
                        TaskDescription = "Annual health check-up at 10 AM",
                        Status = TaskStatusEnum.PENDING,
                        Priority = TaskPriorityEnum.MEDIUM,
                        Category = TaskCategoryEnum.PERSONAL,
                        CreatedAt = DateTime.UtcNow.AddDays(-1),
                        UpdatedAt = DateTime.UtcNow
                    },
                    new TaskEntity
                    {
                        TaskId = 5,
                        UserId = 5,
                        TaskTitle = "Read AI research paper",
                        TaskDescription = "Read the latest research on GPT models",
                        Status = TaskStatusEnum.PENDING,
                        Priority = TaskPriorityEnum.LOW,
                        Category = TaskCategoryEnum.EDUCATION,
                        CreatedAt = DateTime.UtcNow.AddDays(-7),
                        UpdatedAt = DateTime.UtcNow.AddDays(-3)
                    },
                    new TaskEntity
                    {
                        TaskId = 6,
                        UserId = 1,
                        TaskTitle = "Update project documentation",
                        TaskDescription = "Add details about the new API endpoints",
                        Status = TaskStatusEnum.IN_PROGRESS,
                        Priority = TaskPriorityEnum.MEDIUM,
                        Category = TaskCategoryEnum.WORK,
                        CreatedAt = DateTime.UtcNow.AddDays(-2),
                        UpdatedAt = DateTime.UtcNow
                    },
                    new TaskEntity
                    {
                        TaskId = 7,
                        UserId = 2,
                        TaskTitle = "Plan weekend trip",
                        TaskDescription = "Decide on destination and book accommodation",
                        Status = TaskStatusEnum.PENDING,
                        Priority = TaskPriorityEnum.LOW,
                        Category = TaskCategoryEnum.PERSONAL,
                        CreatedAt = DateTime.UtcNow.AddDays(-10),
                        UpdatedAt = DateTime.UtcNow.AddDays(-5)
                    }
                };

                context.Tasks.AddRange(tasks);
                context.SaveChanges();
            }
        }
    }
}
