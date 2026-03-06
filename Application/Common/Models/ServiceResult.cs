using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ServiceResult
    {
        public bool Succeeded { get; init; }
        public string[] Errors { get; init; } = Array.Empty<string>();

        public static ServiceResult Success() => new() { Succeeded = true };
        public static ServiceResult Failure(IEnumerable<string> errors) => new() { Succeeded = false, Errors = errors.ToArray() };
    }
}
