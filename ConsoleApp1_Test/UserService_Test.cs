using ConsoleApp1.Interfaces;
using ConsoleApp1.Models;
using ConsoleApp1.Services;

namespace ConsoleApp1_Test
{
    public class UserService_Test
    {
        [Fact]
        public void AddToListShould_AddOneUserToUserList_ThenReturnTrue()
        {
            IUserInfo customer = new UserInfo { FirstName="", LastName="",Email="" };
            IUserService userService = new UserService();
            userService.AddUsertoList(customer);

            IEnumerable<IUserInfo> result = userService.GetUserToList();



        }
    }
}
