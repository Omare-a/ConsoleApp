using ConsoleApp1.Interfaces;
using ConsoleApp1.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace ConsoleApp1.Services;

public interface IMenuService
{
    void ShowMenu();
   
}
public class MenuService: IMenuService
{
    private readonly IUserService _userService = new UserService();


    public void ShowMenu()
    {
        while (true)
        {
            DisplayTitle("MENY OPTIONS");
            Console.WriteLine($"{"1", -4} Add New Customer");
            Console.WriteLine($"{"2",-4} View Customer List");
            Console.WriteLine($"{"3",-4} Delete User by Email");
            Console.WriteLine($"{"0",-4} Exit Application");

            Console.WriteLine();
            Console.WriteLine("Enter Meny Option");
            var option = Console.ReadLine();


            switch (option)
            {
                case "1":
                    ShowAddUserOption();
                    break;
                case "2":
                    ShowViewUserListOption(); 
                    break;
                case "3":
                    ShowDeleteUserOption();
                    break;
                case "0":
                    ShowExitApplicationOption();
                    break;
                default:
                    Console.WriteLine("Please Try Again");
                    Console.ReadKey();
                    break;


            }


        }

    }

    private void ShowExitApplicationOption()
    {
        Console.Clear();
        Console.Write("Are you sure you want to close this application (Y/N): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
            Environment.Exit(0);
    }

    private void ShowAddUserOption()
    {
        IUserInfo customer = new UserInfo();

        DisplayTitle("Add New User");
        Console.Write("First Name: ");
        customer.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        customer.LastName = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        customer.PhoneNumber = Console.ReadLine()!;

        Console.Write("E-mail: ");
        customer.Email = Console.ReadLine()!;

        Console.Write("Adress: ");
        customer.Adress = Console.ReadLine()!;

        var res = _userService.AddUsertoList(customer);

        switch (res.Status)
        {
            case Enums.ServiceStatus.SUCCESS:
                Console.WriteLine("User has been added successfully. ");
                break;

            case Enums.ServiceStatus.ALREADY_EXIST:
                Console.WriteLine("The user already exist. ");
                break;

            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("Failed to add the list");
                Console.WriteLine("See error massage:: " + res.Result.ToString());
                break;
            case Enums.ServiceStatus.DELETED:
                Console.WriteLine("user has been deleted");
                break;
        }

        DisplayPressAnyKey();
    }

    private void ShowViewUserListOption()
    {
        DisplayTitle("Show added user list");
        var res = _userService.GetUserToList();

        if (res.Status == Enums.ServiceStatus.SUCCESS)
        {
            if(res.Result is List<IUserInfo> userList)
            {
                if (!userList.Any())
                {
                    Console.WriteLine("No user found");
                }
                else 
                {
                    string json = JsonConvert.SerializeObject(userList, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    Console.WriteLine(json);

                    foreach (var users in userList)
                    {
                        
                        Console.WriteLine($"{users.FirstName} {users.LastName} <{users.Email}> ");
                        
                    }
                }
            }
        }
        DisplayPressAnyKey();

        
    }

    private void ShowDeleteUserOption()
    {
        DisplayTitle("Delete User by Email");
        Console.Write("Enter the email address to delete the user: ");
        var emailToDelete = Console.ReadLine();

        var deleteResult = _userService.DeleteUserByEmail(emailToDelete!);

        switch (deleteResult.Status)
        {
            case Enums.ServiceStatus.DELETED:
                Console.WriteLine("User deleted successfully.");
                break;
            case Enums.ServiceStatus.NOT_FOUND:
                Console.WriteLine("User not found.");
                break;
            case Enums.ServiceStatus.FAILED:
                Console.WriteLine($"Failed to delete user. Error: {deleteResult.Result}");
                break;
            default:
                Console.WriteLine("Unexpected error.");
                break;
        }

        DisplayPressAnyKey();
    }

    private void DisplayTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"## {title} ##");
        Console.WriteLine();
    }
    private void DisplayPressAnyKey()
    {
        Console.WriteLine();
        Console.WriteLine("Press Any key to continue.");
        Console.ReadKey();

    }
}
