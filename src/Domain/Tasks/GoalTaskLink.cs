using Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class GoalTaskLink
    {
        public int Id { get; set; }
        public int GoalId { get; set; }
        public ProjectGoal Goal { get; set; } = null!;
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public decimal? weight { get; set; }

    }
}
