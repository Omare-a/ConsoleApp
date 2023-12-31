using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Models;

public class UserInfo : IUserInfo
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Adress { get; set; } = null!;
    public Guid id { get; set; } = Guid.NewGuid();
}
