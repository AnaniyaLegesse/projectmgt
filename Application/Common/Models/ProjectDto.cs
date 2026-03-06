using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsArchived { get; set; }

        // Metadata for the UI
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
    }
}
