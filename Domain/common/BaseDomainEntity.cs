using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }
    }
}
