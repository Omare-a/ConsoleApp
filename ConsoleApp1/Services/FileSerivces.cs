using System.Diagnostics;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Services;

public class FileSerivces : IFileSerivces
{

    public bool SaveContentToFile(string content, string filePath)
    {
        try
        {
            using(var sw  = new StreamWriter(filePath))
            {
                sw.Write(content);
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public string GetContentFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

}
