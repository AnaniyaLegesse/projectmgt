using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(int id, CancellationToken ct);

        Task<Project?> GetByIdWithTasksAsync(int id, CancellationToken ct);
        Task<List<Project>> GetAllAsync(CancellationToken ct);
        void Add(Project project);
        void Update(Project project);
        void Delete(Project project);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
