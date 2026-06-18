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
        public string? Description { get; private set; }
        public DateTime? Deadline { get; private set; }

        public int Score {  get; private set; }

        public Team Team { get; private set; }

        // 1. State tracking
        public bool IsArchived { get; private set; }

        // 2. Encapsulated collection
        private readonly List<ProjectTask> _tasks = new();
        public virtual IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();

        // Constructor ensures a valid object state from the start
        public Project(string name, string? description = null, DateTime? deadline = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name is required.");

            Name = name;
            Description = description;
            Deadline = deadline;
            IsArchived = false;
        }

        // 3. Update logic
        public void UpdateDetails(string name, string? description, DateTime? deadline)
        {
            if (IsArchived)
                throw new InvalidOperationException("Cannot update an archived project.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name cannot be empty.");

            Name = name;
            Description = description;
            Deadline = deadline;
        }

        // 4. The Archive Business Rule
        public void Archive()
        {
            // Rule: Cannot archive if there are any tasks not marked as 'Done'
            if (_tasks.Any(t => t.Status != ProjectTaskStatus.Done))
            {
                throw new InvalidOperationException("Cannot archive a project with incomplete tasks.");
            }

            IsArchived = true;
        }

        // 5. Aggregate logic: Adding a task through the project
        public void AddTask(string title, string? description)
        {
            if (IsArchived)
                throw new InvalidOperationException("Cannot add tasks to an archived project.");

            // Logic to create and add task could go here
            // _tasks.Add(new ProjectTask(title, description, this.Id));
        }
    }

}