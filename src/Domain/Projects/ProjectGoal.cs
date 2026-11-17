namespace Domain.Projects
{
    public class ProjectGoal
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public int? ParentId { get; set; }
        public ProjectGoal? Parent { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal TargetValue { get; private set; }
        public decimal CurrentValue { get; private set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public void UpdateProgress(decimal targetValue, decimal currentValue)
        {
            if (targetValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(targetValue), "The target value cannot be negative.");
            }

            if (currentValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(currentValue), "The current value cannot be negative.");
            }

            TargetValue = targetValue;
            CurrentValue = currentValue;
        }
    }
}
