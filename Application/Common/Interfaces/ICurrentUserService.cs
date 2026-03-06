using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; } // Returns the ID of the user making the request
    }
}
