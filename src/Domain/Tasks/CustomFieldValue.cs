using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class CustomFieldValue
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public int FieldDefinationId { get; set; }
        public CustomFieldDefination FieldDefination { get; set; } = null!;

        [Column(TypeName = "jsonb")]
        public string Value { get; set; } = null!;
    }
}
