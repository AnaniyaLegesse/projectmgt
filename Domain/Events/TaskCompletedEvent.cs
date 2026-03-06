using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Event
{
    public record TaskCompletedEvent(int TaskId, int AssigneeId, DateTime CompletedAt);
}
