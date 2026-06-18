using Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseDomainEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }

        public int Score {  get; set; }

        // Relationship: Tasks assigned to this user
        public List<ProjectTask> AssignedTasks { get; set; } = new();

        // Relationship: Comments written by this user
        public List<Comment> Comments { get; set; } = new();

        // Relationship: Projects this user owns/created
        // (Optional: useful if you want to track who can delete a project)
        public List<Project> OwnedProjects { get; set; } = new();

        public List<Team> Teams { get; set; } = new();
    }
}
