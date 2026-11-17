using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Projects
{
    public class ProjectDeliverable
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
       
        public int MilstoneId { get; set; }
        public ProjectMilstone Milstone { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    
        public DateTime DueDate { get; set; }
    }
}
