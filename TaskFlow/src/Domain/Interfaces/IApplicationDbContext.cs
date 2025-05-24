using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces;


public interface IApplicationDbContext
{
    IQueryable<User> Users { get; }
    IQueryable<Project> Projects { get; }
    IQueryable<TaskItem> Tasks { get; }
    IQueryable<Team> Teams { get; }
    IQueryable<TeamMember> TeamMembers { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}