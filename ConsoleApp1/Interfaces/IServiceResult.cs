using ConsoleApp1.Enums;
using ConsoleApp1.Models.Responses;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1.Interfaces;
/// <summary>
///  represents the result of a service operation, providing information about the operations status
/// </summary>
public interface IServiceResult
{
    ServiceStatus Status { get; set; }
    object Result { get; set; }

}
