using Domain.Organizations;
using Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    
        public int PhaseId { get; set; }
        public ProjectPhase Phase { get; set; } = null!;

        public int MilstoneId { get; set; }
        public ProjectMilstone Milstone { get; set; } = null!;

        public int ParentTaskId { get; set; }
        public Task ParentTask { get; set; } = null!;

        public int StatusId { get; set; }
        public TaskStatus Status { get; set; } = null!;

        public int PriorityId { get; set; }
        public TaskPriorities Priority { get; set; } = null!;

        public int ReporterId { get; set; }
        public User Reporter { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal? Estimate { get; set; }
        public string SortKey { get; set; } = string.Empty;
    }
    
}
