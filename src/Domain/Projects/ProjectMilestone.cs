namespace Domain.Projects
{
    public class ProjectMilestone
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public int PhaseId { get; set; }
        public ProjectPhase Phase { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string? Status { get; set; }
    }
}
