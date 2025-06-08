using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Model;
using ForDoListApp.Data;

namespace Models.Service.History
{
    public class HistoryService : ITaskHistoryService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HistoryService> _logger;

        public HistoryService(AppDbContext context, ILogger<HistoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<TaskHistoryEntity> GetHistoryByTaskId(int taskId)
        {
            if (taskId < 1)
            {
                _logger.LogError("GetHistoryByTaskId - invalid taskId.");
                return new List<TaskHistoryEntity>();
            }

            var history = _context.TaskHistories
                                .Where(h => h.TaskId == taskId)
                                .OrderByDescending(h => h.ChangedAt)
                                .ToList();

            _logger.LogInformation($"Fetched {history.Count} history records for task {taskId}.");

            return history;
        }

        public void SaveHistory(TaskHistoryEntity history)
        {
            if (history == null)
            {
                _logger.LogError("SaveHistory - history is null.");
                return;
            }

            _context.TaskHistories.Add(history);
            _context.SaveChanges();

            _logger.LogInformation($"History record saved for task {history.TaskId} by user {history.UserId}.");
        }
    }
}
