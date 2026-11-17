using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class SprintReport
    {
        public int Id { get; set; }
        public int SprintId { get; set; }   
        public Sprint Sprint { get; set; } = null!;
        public decimal CommitteddPoints { get; set; }
        public decimal CompletedPoints { get; set; }
        public decimal Velocity { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    }
}
