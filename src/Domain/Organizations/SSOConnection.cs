using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class SSOConnection
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;
        public string Protocol { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
       
        [Column(TypeName = "jsonb")]
        public string Config { get; set; } = string.Empty;

        public Boolean IsActive { get; set; } = false;


    }
}
