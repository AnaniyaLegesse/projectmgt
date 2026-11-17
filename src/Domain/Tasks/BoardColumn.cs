using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class BoardColumn
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
  
        public int SortOrder { get; set; } = 0;
        public int WipLimit { get; set; } = 0;

    }
}
