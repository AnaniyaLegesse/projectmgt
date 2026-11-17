using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class BoardTask
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;
        public int ColumnId { get; set; }
        public BoardColumn Column { get; set; } = null!;
        public int TraskId { get; set; }

        public Task Task { get; set; } = null!;
        public string Rank { get; set; } = string.Empty;

    }
}
