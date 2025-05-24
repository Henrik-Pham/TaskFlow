namespace TaskFlow.Application.Common.Models;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public Guid? TeamId { get; set; }
    public string? TeamName { get; set; }
    public int TaskCount { get; set; }
    public DateTime CreatedAt { get; set; }
}