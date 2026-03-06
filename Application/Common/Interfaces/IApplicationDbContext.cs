using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    // Avoid depending on EF Core types in the Application project.
    public interface IApplicationDbContext
    {
        IQueryable<Project> Projects { get; }
        IQueryable<ProjectTask> Tasks { get; }
        IQueryable<User> Users { get; }
        IQueryable<Comment> Comments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
