using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks;

public class TaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    private readonly IApplicationDbContext _context;

    public TaskCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(e => e.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed more than 200 characters");

        RuleFor(e => e.Description)
            .MaximumLength(2000).WithMessage("Description cannot be longer than 2000 characters");

        RuleFor(e => e.ProjectId)
            .NotEmpty().WithMessage("A project is required")
            .MustAsync(ProjectExists).WithMessage("A project already exists");

        RuleFor(e => e.AssigneeId)
            .MustAsync(UserExistsWhenProvided).WithMessage("Assignee must be provided")
            .When(e => e.AssigneeId.HasValue);

        RuleFor(e => e.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future")
            .When(e => e.DueDate.HasValue);
    }
    
    private async Task<bool> ProjectExists(Guid projectId, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AnyAsync(p => p.Id == projectId && p.IsActive, cancellationToken);
    }

    private async Task<bool> UserExistsWhenProvided(Guid? userId, CancellationToken cancellationToken)
    {
        if (!userId.HasValue) return true;
        
        return await _context.Users
            .AnyAsync(u => u.Id == userId.Value && u.IsActive, cancellationToken);
    }
}