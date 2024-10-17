using Shared.Enums;
using Shared.Interfaces;

namespace Shared.Services;

public class FileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;

    public StatusCodes SaveToFile(string content)
    {
        try
        {
            using var sw = new StreamWriter(_filePath);
            sw.WriteLine(content);

            return StatusCodes.Success;
        }
        catch
        {
            return StatusCodes.Failed;
        }
    }

    public string LoadFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using var sr = new StreamReader(_filePath);
                return sr.ReadToEnd();
            }

            return null!;
        }
        catch
        {
            return null!;
        }
    }
}
