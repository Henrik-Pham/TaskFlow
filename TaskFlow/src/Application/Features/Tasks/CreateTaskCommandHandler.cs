using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Common.Models;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateTaskCommandHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Get next order number for the project
        var maxOrder = await _context.Tasks
            .Where(t => t.ProjectId == request.ProjectId)
            .MaxAsync(t => (int?)t.Order, cancellationToken) ?? 0;

        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            DueDate = request.DueDate,
            ProjectId = request.ProjectId,
            AssigneeId = request.AssigneeId,
            Order = maxOrder + 1,
            CreatedBy = _currentUserService.UserId ?? "System",
            UpdatedBy = _currentUserService.UserId ?? "System"
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        // Load related data for mapping
        var createdTask = await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.Assignee)
            .FirstAsync(t => t.Id == task.Id, cancellationToken);

        return _mapper.Map<TaskDto>(createdTask);
    }
}