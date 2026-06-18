using Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Team: BaseDomainEntity
    {
        public String Name { get; set;}
        public String Description { get; set;}

        // Relationship: A team has many members (Users)
        public List<User> Members { get; set; } = new();

        // Relationship: A team manages many projects
        public List<Project> Projects { get; set; } = new();



    }
}
