namespace ConsoleApp1.Interfaces;

public interface IFileSerivces
{
    /// <summary>
    /// save content to a filepath
    /// </summary>
    /// <param name="filePath">enter the filepath with extenstion (myC-Project)</param>
    /// <param name="content">enter the content as a string</param>
    /// <returns>returns true if it is saved, else false if failed</returns>
    bool SaveContentToFile(string filePath, string content);


    /// <summary>
    /// Get content as string from a specified filepath
    /// </summary>
    /// <param name="filePath">enter the filepath with extenstion (myC-Project)</param>
    /// <returns>returns file content as string if file exist, else returns null</returns>
    string GetContentFromFile(string filePath);

}
