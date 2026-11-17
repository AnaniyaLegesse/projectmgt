using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string KeyHash { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
       
        [Column(TypeName = "jsonb")]
        public string ? Scope { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
       
        public int CreatedById { get; set; }

        public User CreatedBy { get; set; } = null!;

    }
}
