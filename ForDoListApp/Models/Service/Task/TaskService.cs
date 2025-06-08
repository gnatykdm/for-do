using System;
using System.Collections;
using Enums;
using ForDoListApp.Data;
using Model;

namespace Models.Service.Task
{
    public class TaskService : ITaskService
    {

        private readonly AppDbContext _context;
        private readonly ILogger<TaskService> _logger;

        public TaskService(AppDbContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public void SaveTask(TaskEntity task)
        {
            if (task == null)
            {
                _logger.LogError("Task[SaveTask] - task is null.");
                return;
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();
            _logger.LogInformation($"Task[SaveTask] - task saved with id: {task.TaskId}.");
        }

    public void UpdateTask(TaskEntity task)
    {
        if (task == null || task.TaskId < 1)
        {
            _logger.LogError("Task[UpdateTask] - invalid task.");
            return;
        }

        var existingTask = _context.Tasks.Find(task.TaskId);
        if (existingTask == null)
        {
            _logger.LogWarning($"Task[UpdateTask] - task with id {task.TaskId} not found.");
            return;
        }

        existingTask.TaskTitle = task.TaskTitle;
        existingTask.TaskDescription = task.TaskDescription;
        existingTask.DueDate = task.DueDate;
        existingTask.Status = task.Status;

        _context.SaveChanges();
        _logger.LogInformation($"Task[UpdateTask] - task updated with id: {task.TaskId}.");
    }

    public void dropTaskById(int taskId)
    {
        if (taskId < 1)
        {
            _logger.LogError("Task[dropTaskById] - invalid taskId.");
            return;
        }

        var task = _context.Tasks.Find(taskId);
        if (task == null)
        {
            _logger.LogWarning($"Task[dropTaskById] - task with id {taskId} not found.");
            return;
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();
        _logger.LogInformation($"Task[dropTaskById] - task with id {taskId} deleted.");
    }

        public List<TaskEntity>? GetAllTaskByUserId(int userId)
        {
            if (userId < 1)
            {
                _logger.LogError("Task[GetTaskByUserId] - userId is invalid.");
                return null;
            }

            var tasks = _context.Tasks
                                .Where(t => t.User.UserId == userId)
                                .ToList();

            if (tasks == null || tasks.Count == 0)
            {
                _logger.LogWarning($"Task[GetAllTaskByUserId] - user {userId} doesn't have any tasks.");
                return null;
            }

            _logger.LogInformation($"Task[GetAllTaskByUserId] - user {userId} fetched {tasks.Count} task(s).");
            return tasks;
        }

        public TaskEntity? GetTaskById(int taskId)
        {
            if (taskId < 1)
            {
                _logger.LogError("Task[GetTaskById] - taskId is invalid.");
                return null;
            }

            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (task == null)
            {
                _logger.LogWarning($"Task[GetTaskById] - task {taskId} not found.");
                return null;
            }

            _logger.LogInformation($"Task[GetTaskById] - fetched task with id: {taskId}.");
            return task;
        }
    }
}