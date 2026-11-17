using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class BaselineItem
    {
        public int Id { get; set; }
        public int BaselineId { get; set; }
        public ProjectBaseline Baseline { get; set; } = null!;

        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;

        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public decimal PlannedEffort { get; set; }



    }
}
