using System;
using System.Linq;
using ForDoListApp.Data;
using Model;
using Enums;
using Microsoft.EntityFrameworkCore;

namespace ForDoListApp.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate();

            // Seed Users
            if (!context.Users.Any())
            {
                var user1 = new UserEntity
                {
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = "hashed_password_1", 
                    CreatedAt = DateTime.UtcNow
                };

                var user2 = new UserEntity
                {
                    Username = "john_doe",
                    Email = "john.doe@example.com",
                    PasswordHash = "hashed_password_2",
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.AddRange(user1, user2);
                context.SaveChanges();

                // Seed Categories
                var category1 = new CategoryEntity
                {
                    UserId = user1.UserId,
                    CategoryName = "Work"
                };

                var category2 = new CategoryEntity
                {
                    UserId = user2.UserId,
                    CategoryName = "Personal"
                };

                context.Categories.AddRange(category1, category2);

                // Seed Priorities
                var priorityLow = new PriorityEntity
                {
                    PriorityName = "Low",
                    PriorityLevel = 1
                };
                var priorityMedium = new PriorityEntity
                {
                    PriorityName = "Medium",
                    PriorityLevel = 3
                };
                var priorityHigh = new PriorityEntity
                {
                    PriorityName = "High",
                    PriorityLevel = 5
                };

                context.Priorities.AddRange(priorityLow, priorityMedium, priorityHigh);

                context.SaveChanges();

                // Seed Tasks
                var task1 = new Model.TaskEntity
                {
                    UserId = user1.UserId,
                    CategoryId = category1.CategoryId,
                    PriorityId = priorityHigh.PriorityId,
                    TaskTitle = "Finish project report",
                    TaskDescription = "Complete the report and submit by Friday.",
                    Color = TaskColor.Red,
                    Status = Enums.TaskStatus.InProgress,
                    DueDate = DateTime.UtcNow.AddDays(5),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var task2 = new Model.TaskEntity
                {
                    UserId = user2.UserId,
                    CategoryId = category2.CategoryId,
                    PriorityId = priorityMedium.PriorityId,
                    TaskTitle = "Buy groceries",
                    TaskDescription = "Milk, Bread, Eggs, and Vegetables",
                    Color = TaskColor.Green,
                    Status = Enums.TaskStatus.Pending,
                    DueDate = DateTime.UtcNow.AddDays(2),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                context.Tasks.AddRange(task1, task2);
                context.SaveChanges();

                // Seed TaskHistory
                var history1 = new TaskHistoryEntity
                {
                    TaskId = task1.TaskId,
                    UserId = user1.UserId,
                    ChangeDescription = "Task created with status in_progress",
                    ChangedAt = DateTime.UtcNow
                };

                var history2 = new TaskHistoryEntity
                {
                    TaskId = task2.TaskId,
                    UserId = user2.UserId,
                    ChangeDescription = "Task created with status pending",
                    ChangedAt = DateTime.UtcNow
                };

                context.TaskHistories.AddRange(history1, history2);
                context.SaveChanges();
            }
        }
    }
}
