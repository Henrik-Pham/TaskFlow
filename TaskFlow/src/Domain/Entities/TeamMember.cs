using TaskFlow.Domain.Common;

namespace TaskFlow.Domain.Entities;

public class TeamMember : BaseEntity
{
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = "Member"; // Member, Lead, Admin
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual Team Team { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}