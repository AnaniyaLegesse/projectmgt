using Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;
     
        public string Name { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
         
        public string Status { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }
        public DateTime? ActualEndDate { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;
    }
    
}
