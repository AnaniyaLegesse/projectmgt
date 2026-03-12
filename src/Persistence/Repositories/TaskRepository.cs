using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class TaskRepository : ITaskRepository
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

    public Task<List<ProjectTask>> GetByProjectIdAsync(int projectId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public void Delete(ProjectTask task)
    {
        throw new NotImplementedException();
    }
}
