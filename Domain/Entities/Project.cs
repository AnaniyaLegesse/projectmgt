using Domain.common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project : BaseDomainEntity
    {
        public string Name { get; private set; }

        // Optional fields (Nullable)
        public string? Description { get; private set; }
        public DateTime? Deadline { get; private set; }

        public Project(string name, string? description = null, DateTime? deadline = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name is required.");

            Name = name;
            Description = description;
            Deadline = deadline;
        }

        // You can add a business rule to set these later
        public void UpdateDetails(string? description, DateTime? deadline)
        {
            Description = description;
            Deadline = deadline;
        }
    }
}
