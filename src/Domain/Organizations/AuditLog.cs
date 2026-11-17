using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;


        public int ActorId { get; set; }
        public User Actor { get; set; } = null!;
       
        public string Action { get; set; } = string.Empty;

        public string TargetType { get; set; } = string.Empty;
        public int TargetId { get; set; }
        
        [Column(TypeName = "jsonb")]
        public string MetaData { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
