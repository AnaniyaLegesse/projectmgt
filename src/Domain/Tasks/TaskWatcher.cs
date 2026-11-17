using Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class TaskWatcher
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
