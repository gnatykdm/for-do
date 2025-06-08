using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Model;
using ForDoListApp.Data;

namespace Models.Service.Priority
{
    public class PriorityService : IPriorityService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PriorityService> _logger;

        public PriorityService(AppDbContext context, ILogger<PriorityService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<PriorityEntity> GetAllPriorities()
        {
            var priorities = _context.Priorities.ToList();
            _logger.LogInformation($"Fetched {priorities.Count} priorities.");
            return priorities;
        }

        public PriorityEntity? GetPriorityById(int priorityId)
        {
            if (priorityId < 1)
            {
                _logger.LogError("GetPriorityById - invalid priorityId.");
                return null;
            }

            var priority = _context.Priorities.Find(priorityId);
            if (priority == null)
            {
                _logger.LogWarning($"Priority with id {priorityId} not found.");
            }
            else
            {
                _logger.LogInformation($"Priority with id {priorityId} fetched.");
            }

            return priority;
        }

        public void SavePriority(PriorityEntity priority)
        {
            if (priority == null)
            {
                _logger.LogError("SavePriority - priority is null.");
                return;
            }

            _context.Priorities.Add(priority);
            _context.SaveChanges();
            _logger.LogInformation($"Priority saved with id: {priority.PriorityId}.");
        }

        public void UpdatePriority(PriorityEntity priority)
        {
            if (priority == null || priority.PriorityId < 1)
            {
                _logger.LogError("UpdatePriority - invalid priority.");
                return;
            }

            var existingPriority = _context.Priorities.Find(priority.PriorityId);
            if (existingPriority == null)
            {
                _logger.LogWarning($"UpdatePriority - priority with id {priority.PriorityId} not found.");
                return;
            }

            existingPriority.PriorityName = priority.PriorityName;
            existingPriority.PriorityLevel = priority.PriorityLevel;

            _context.SaveChanges();
            _logger.LogInformation($"Priority updated with id: {priority.PriorityId}.");
        }

        public void DeletePriorityById(int priorityId)
        {
            if (priorityId < 1)
            {
                _logger.LogError("DeletePriorityById - invalid priorityId.");
                return;
            }

            var priority = _context.Priorities.Find(priorityId);
            if (priority == null)
            {
                _logger.LogWarning($"DeletePriorityById - priority with id {priorityId} not found.");
                return;
            }

            _context.Priorities.Remove(priority);
            _context.SaveChanges();
            _logger.LogInformation($"Priority with id {priorityId} deleted.");
        }
    }
}
