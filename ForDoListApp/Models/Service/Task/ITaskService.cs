using Models.Entity;

namespace Models.Service.Task
{
    public interface ITaskService
    {
        List<TaskEntity> GetAllTaskByUserId(int userId);
        TaskEntity GetTaskById(int id);
        void SaveTask(TaskEntity task);
        void UpdateTask(TaskEntity task);
        void DeleteTask(int id);
    }
}