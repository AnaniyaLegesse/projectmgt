using Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class CustomFieldDefination
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string FieldType { get; set; } = null!;
        public string? Options { get; set; }
        public Boolean IsRequired { get; set; } = false;
    }
}
