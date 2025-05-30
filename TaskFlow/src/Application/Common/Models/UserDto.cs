namespace TaskFlow.Application.Common.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public bool isActive { get; set; }
    public DateTime CreatedAt { get; set; }
}