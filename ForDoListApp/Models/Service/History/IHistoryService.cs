using System.Collections.Generic;
using Model;

namespace Models.Service.History
{
    public interface ITaskHistoryService
    {
        List<TaskHistoryEntity> GetHistoryByTaskId(int taskId);
        void SaveHistory(TaskHistoryEntity history);
    }
}