namespace ConsoleApp1.Interfaces
{
    /// <summary>
    /// represents a user personal information.
    /// </summary>
    public interface IUserInfo
    {
        string Adress { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? PhoneNumber { get; set; }
        Guid Id { get; set; }
    }
}