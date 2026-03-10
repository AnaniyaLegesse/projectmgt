using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{

    public class ProjectTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty; // Converted from Enum
        public string Priority { get; set; } = string.Empty; // From Value Object
        public DateTime? DueDate { get; set; }

        // Linked Data
        public int ProjectId { get; set; }
        public UserSummaryDto? Assignee { get; set; }
    }
}
