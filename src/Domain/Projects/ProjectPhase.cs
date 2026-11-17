namespace Domain.Projects
{
    public class ProjectPhase
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public void Reschedule(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("The phase start date must be on or before its end date.", nameof(startDate));
            }

            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
