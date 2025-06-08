using System;
using System.Collections;
using Model;

namespace Models.Service.Task
{
    public interface ITaskService
    {
        void SaveTask(TaskEntity task);
        void UpdateTask(TaskEntity task);
        void dropTaskById(int taskId);
        TaskEntity? GetTaskById(int taskId);
        List<TaskEntity>? GetAllTaskByUserId(int userId);
    }
}