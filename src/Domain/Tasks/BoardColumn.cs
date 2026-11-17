namespace Domain.Tasks
{
    public class BoardColumn
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public int WipLimit { get; private set; }

        public void SetWipLimit(int wipLimit)
        {
            if (wipLimit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(wipLimit), "The WIP limit cannot be negative.");
            }

            WipLimit = wipLimit;
        }
    }
}
