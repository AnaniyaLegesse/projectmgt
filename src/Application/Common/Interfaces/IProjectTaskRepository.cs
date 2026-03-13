using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IProjectTaskRepository
    {
        Task<ProjectTask?> GetByIdAsync(int id, CancellationToken ct);
        Task<List<ProjectTask>> GetByProjectIdAsync(int projectId, CancellationToken ct);
        void Add(ProjectTask task);
        void Delete(ProjectTask task);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
