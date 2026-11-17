using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Projects
{
    public class ProjectGoal
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public int? ParentId { get; set; }
        public ProjectGoal? Parent { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TargetValue { get; set; }
        public int CurrentValue { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
