using Domain.Organizations;
using Domain.Projects;

namespace Domain.Tasks
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int PhaseId { get; set; }
        public ProjectPhase Phase { get; set; } = null!;

        public int MilestoneId { get; set; }
        public ProjectMilestone Milestone { get; set; } = null!;

        public int? ParentTaskId { get; set; }
        public Task? ParentTask { get; set; }

        public int StatusId { get; set; }
        public TaskStatus Status { get; set; } = null!;

        public int PriorityId { get; set; }
        public TaskPriorities Priority { get; set; } = null!;

        public int ReporterId { get; set; }
        public User Reporter { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal? Estimate { get; private set; }
        public string SortKey { get; set; } = string.Empty;

        public void Schedule(DateTime startDate, DateTime dueDate)
        {
            if (startDate > dueDate)
            {
                throw new ArgumentException("The task start date must be on or before its due date.", nameof(startDate));
            }

            StartDate = startDate;
            DueDate = dueDate;
        }

        public void SetEstimate(decimal? estimate)
        {
            if (estimate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(estimate), "The estimate cannot be negative.");
            }

            Estimate = estimate;
        }
    }
}
