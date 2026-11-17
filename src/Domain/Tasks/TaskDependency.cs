using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class TaskDependency
    {
        public int Id { get; set; }
        public int PredecessorTaskId { get; set; }
        public Task PredecessorTask { get; set; } = null!;
        public int SuccessorTaskId { get; set; }
        public Task SuccessorTask { get; set; } = null!;
        public string Type { get; set; } = string.Empty;
        public int LagMinutes { get; set; } = 0;
    }
}
