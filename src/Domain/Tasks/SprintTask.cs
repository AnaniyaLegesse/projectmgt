using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class SprintTask
    {
        public int Id { get; set; }
        public int SprintId { get; set; }
        public Sprint Sprint { get; set; } = null!;
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
