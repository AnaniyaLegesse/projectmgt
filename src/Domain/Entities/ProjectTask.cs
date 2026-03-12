using Domain.common;
using Domain.Enums;
using Domain.Event;
using Domain.ValueObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProjectTask : BaseDomainEntity
    {
        // Private setters enforce encapsulation
        public string Title { get; private set; }
        public ProjectTaskStatus Status { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime? DueDate { get; private set; }

        public int ProjectId { get; private set; }
        public int? AssigneeId { get; private set; }

        
        private readonly List<object> _domainEvents = new();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        private ProjectTask() { }
        public ProjectTask(string title, Priority taskPriority, int projectId)
        {
            Title = title;
            Priority = taskPriority; 
            ProjectId = projectId;
            Status = ProjectTaskStatus.Todo;
        }


        public void Complete(int userId)
        {
            if (Status == ProjectTaskStatus.Done) return;

            if (AssigneeId == null)
                throw new InvalidOperationException("Cannot complete a task that has no assignee.");

            Status = ProjectTaskStatus.Done;

            // Trigger Domain Event
            _domainEvents.Add(new TaskCompletedEvent(Id, AssigneeId.Value, DateTime.UtcNow));
        }

        public void UpdatePriority(Priority newPriority)
        {
            Priority = newPriority;
        }
    }
}
