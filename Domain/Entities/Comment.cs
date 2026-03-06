using Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment : BaseDomainEntity
    {
        public string Text { get; set; } = string.Empty;

        // Link to the Task
        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; } = null!;

        // Link to the Author
        public int AuthorId { get; set; }
        public User Author { get; set; } = null!;
    }
}
