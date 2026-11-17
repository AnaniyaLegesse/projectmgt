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
        public decimal TargetValue { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
