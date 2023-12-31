using ConsoleApp1.Interfaces;
using ConsoleApp1.Models;
using ConsoleApp1.Models.Responses;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsoleApp1.Services;
public class UserService : IUserService
{
    private List<IUserInfo> _users = [];
    private readonly IFileSerivces _fileService = new FileSerivces();
    private readonly string _filePath = @"C:\myC-Project\content.json";

    public IServiceResult AddUsertoList(IUserInfo userInfo)
    {
        IServiceResult response = new ServiceResult();

        try
        {
            if (!_users.Any(x => x.Email == userInfo.Email))
            {
                _users.Add(userInfo);
                var json = JsonConvert.SerializeObject(userInfo, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All});
                var result = _fileService.SaveContentToFile(_filePath, json);
                response.Status = Enums.ServiceStatus.SUCCESS;
                
            }
            else
            {
                response.Status = Enums.ServiceStatus.ALREADY_EXIST;
            }

        }
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex.Message); 
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        
        }
        return response;
    }

    public IEnumerable<IUserInfo> GetUsersFromList() 
    {
        try
        {
            var content = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(content))
            {
                _users = JsonConvert.DeserializeObject<List<IUserInfo>>(content, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
                return _users;
            }

        }
        catch (Exception ex) {Debug.WriteLine(ex.Message);}
        return _users;

    }

    public IServiceResult GetUserToList()
    {
        var response = new ServiceResult();
        try
        {
            GetUsersFromList();
            response.Status = Enums.ServiceStatus.SUCCESS;
            response.Result = _users;
           

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }
        return response;


    }
    public IServiceResult DeleteUserByEmail(string email)
    {
        IServiceResult response = new ServiceResult();

        try
        {
            GetUsersFromList();

            var userToRemove = _users.FirstOrDefault(x => x.Email == email);

            if (userToRemove != null)
            {
                _users.Remove(userToRemove);

                var json = JsonConvert.SerializeObject(_users, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                _fileService.SaveContentToFile(_filePath, json);

                response.Status = Enums.ServiceStatus.DELETED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.NOT_FOUND;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }


}
