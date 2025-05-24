using TaskFlow.Domain.Common;

namespace TaskFlow.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid OwnerId { get; set; }
    public Guid? TeamId { get; set; }
    
    // Navigation properties
    public virtual User Owner { get; set; } = null!;
    public virtual Team? Team { get; set; }
    public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}