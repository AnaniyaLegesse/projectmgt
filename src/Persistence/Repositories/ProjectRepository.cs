using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<Project?> GetByIdWithTasksAsync(int id, CancellationToken ct)
    {
        // We 'Include' the tasks so the Domain Archive logic has the data it needs
        return await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public void Add(Project project)
    {
        _context.Projects.Add(project);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }

    public Task<List<Project>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public void Update(Project project)
    {
        throw new NotImplementedException();
    }
}