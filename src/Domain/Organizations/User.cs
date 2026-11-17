using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Organizations
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string ? PasswordHash { get; set; }

        public string? AvatarUrl { get; set; }

        public string ? timezone { get; set; }

        public string? status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
