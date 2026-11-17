using Domain.Projects;

namespace Domain.Tasks
{
    public class CustomFieldDefinition
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public string? Options { get; set; }
        public bool IsRequired { get; set; }
    }
}
