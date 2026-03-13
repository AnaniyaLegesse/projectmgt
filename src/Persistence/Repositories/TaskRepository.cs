using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TaskRepository : IProjectTaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectTask?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public void Add(ProjectTask task) => _context.Tasks.Add(task);

    public void Update(ProjectTask task) => _context.Tasks.Update(task);

    public async Task SaveChangesAsync(CancellationToken ct) => await _context.SaveChangesAsync(ct);

    public async Task<List<ProjectTask>> GetByProjectIdAsync(int projectId, CancellationToken ct)
    {
        // We use Where to filter and AsNoTracking for a performance boost on reads
        return await _context.Tasks
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId)
            .ToListAsync(ct);
    }

    public void Delete(ProjectTask task)
    {
        // This marks the entity for deletion; it will be removed when SaveChangesAsync is called
        _context.Tasks.Remove(task);
    }
}