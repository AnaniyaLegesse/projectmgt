using Domain.Organizations;
using Domain.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class SavedView
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;
        public string Name { get; set; } = string.Empty;

        public string ViewType { get; set; } = string.Empty;
        
        [Column(TypeName = "jsonb")]
        public string FilterJson { get; set; } = string.Empty;
        public Boolean IsShared { get; set; } = false;

    }
}
