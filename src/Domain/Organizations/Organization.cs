using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string slug { get; set; } = string.Empty;
        public string ? plan { get; set; }
        public string ? Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
