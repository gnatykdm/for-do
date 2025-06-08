using System;
using System.Collections;
using Model;

namespace Models.Service.Priority
{
    public interface IPriorityService
    {
        List<PriorityEntity> GetAllPriorities();
        PriorityEntity? GetPriorityById(int priorityId);
        void SavePriority(PriorityEntity priority);
        void UpdatePriority(PriorityEntity priority);
        void DeletePriorityById(int priorityId);
    }
}