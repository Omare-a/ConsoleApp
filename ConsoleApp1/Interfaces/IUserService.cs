using ConsoleApp1.Models;
using ConsoleApp1.Models.Responses;

namespace ConsoleApp1.Interfaces;

//create read update delete

public interface IUserService
{
    IServiceResult AddUsertoList(IUserInfo userInfo);
    IServiceResult GetUserToList();
    IServiceResult DeleteUserByEmail(string email);

}
