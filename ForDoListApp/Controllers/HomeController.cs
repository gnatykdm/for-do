using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ForDoListApp.Models;
using Models.Entity;
using Models.Service.Task;
using System;
using System.Collections.Generic;
using Models.Entity.Enums;
using System.Linq;

namespace Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskService _taskService;

        public HomeController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpGet]
        public IActionResult Index(string filterStatus = null, string sortBy = "DueDate")
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var tasks = _taskService.GetAllTaskByUserId(userId.Value) ?? new List<TaskEntity>();

            // Apply filtering
            if (!string.IsNullOrEmpty(filterStatus) && Enum.TryParse<TaskStatusEnum>(filterStatus, out var status))
            {
                tasks = tasks.Where(t => t.Status == status).ToList();
            }

            // Apply sorting
            tasks = sortBy switch
            {
                "Priority" => tasks.OrderBy(t => t.Priority).ToList(),
                "Category" => tasks.OrderBy(t => t.Category).ToList(),
                _ => tasks.OrderBy(t => t.DueDate).ToList()
            };

            var viewModel = new TasksViewModel
            {
                Tasks = tasks,
                NewTask = new TaskEntity(),
                FilterStatus = filterStatus,
                SortBy = sortBy
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NewTask")] TasksViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                Console.WriteLine("User not logged in, redirecting to Login");
                return RedirectToAction("Login", "Account");
            }

            // Ensure NewTask is initialized
            model.NewTask ??= new TaskEntity();
            model.Tasks ??= new List<TaskEntity>();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("ModelState Errors: " + string.Join(", ", errors));
                model.Tasks = _taskService.GetAllTaskByUserId(userId.Value) ?? new List<TaskEntity>();
                return View("Index", model);
            }

            try
            {
                var task = model.NewTask;
                Console.WriteLine($"Attempting to save task: Title={task.TaskTitle}, UserId={userId.Value}, TaskId={task.TaskId}");
                task.Status = TaskStatusEnum.PENDING;
                // Let database handle CreatedAt and UpdatedAt via CURRENT_TIMESTAMP
                if (task.DueDate.HasValue)
                {
                    task.DueDate = DateTime.SpecifyKind(task.DueDate.Value, DateTimeKind.Utc); // Ensure UTC
                }
                task.UserId = userId.Value;
                task.User = null; // Explicitly set to null since it's not needed

                _taskService.SaveTask(task);
                Console.WriteLine($"Task saved successfully: TaskId={task.TaskId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving task: {ex.Message}, StackTrace: {ex.StackTrace}");
                ModelState.AddModelError(string.Empty, "Error saving task. Please try again.");
                model.Tasks = _taskService.GetAllTaskByUserId(userId.Value) ?? new List<TaskEntity>();
                return View("Index", model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                Console.WriteLine("User not logged in, redirecting to Login");
                return RedirectToAction("Login", "Account");
            }

            var task = _taskService.GetTaskById(id);
            if (task == null || task.UserId != userId.Value)
            {
                Console.WriteLine($"Task not found or not owned by user: TaskId={id}, UserId={userId.Value}");
                return NotFound();
            }

            var viewModel = new TasksViewModel
            {
                NewTask = task,
                Tasks = _taskService.GetAllTaskByUserId(userId.Value) ?? new List<TaskEntity>()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("NewTask")] TasksViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                Console.WriteLine("User not logged in, redirecting to Login");
                return RedirectToAction("Login", "Account");
            }

            // Ensure NewTask is initialized
            model.NewTask ??= new TaskEntity();
            model.Tasks ??= new List<TaskEntity>();

            // Validate TaskId
            if (model.NewTask.TaskId <= 0)
            {
                Console.WriteLine("Invalid TaskId for edit");
                ModelState.AddModelError("NewTask.TaskId", "Invalid task ID.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("ModelState Errors: " + string.Join(", ", errors));
                model.Tasks = _taskService.GetAllTaskByUserId(userId.Value) ?? new List<TaskEntity>();
                return View(model);
            }

            try
            {
                var task = model.NewTask;
                Console.WriteLine($"Attempting to update task: TaskId={task.TaskId}, Title={task.TaskTitle}, UserId={userId.Value}");

                // Verify task exists and is owned by the user
                var existingTask = _taskService.GetTaskById(task.TaskId);
                if (existingTask == null || existingTask.UserId != userId.Value)
                {
                    Console.WriteLine($"Task not found or not owned by user: TaskId={task.TaskId}, UserId={userId.Value}");
                    return NotFound();
                }

                // Ensure DueDate is UTC
                if (task.DueDate.HasValue)
                {
                    task.DueDate = DateTime.SpecifyKind(task.DueDate.Value, DateTimeKind.Utc);
                }

                task.UserId = userId.Value;
                task.User = null; // Avoid navigation property issues
                task.CreatedAt = existingTask.CreatedAt; // Preserve original CreatedAt

                _taskService.UpdateTask(task);
                Console.WriteLine($"Task updated successfully: TaskId={task.TaskId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating task: {ex.Message}, StackTrace: {ex.StackTrace}");
                ModelState.AddModelError(string.Empty, "Error updating task. Please try again.");
                model.Tasks = _taskService.GetAllTaskByUserId(userId.Value) ?? new List<TaskEntity>();
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var task = _taskService.GetTaskById(id);
                if (task == null || task.UserId != userId.Value)
                {
                    return NotFound();
                }

                _taskService.DeleteTask(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting task: " + ex.Message);
                TempData["Error"] = "Error deleting task. Please try again.";
            }

            return RedirectToAction("Index");
        }
    }
}