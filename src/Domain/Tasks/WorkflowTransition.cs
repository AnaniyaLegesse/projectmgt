using Domain.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class WorkflowTransition
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int FromStatusId { get; set; }
        public TaskStatus FromStatus { get; set; } = null!;
        public int ToStatusId { get; set; }
        public TaskStatus ToStatus { get; set; } = null!;

        [Column(TypeName = "jsonb")]
        public string Conditions { get; set; } = string.Empty;
    }
}
