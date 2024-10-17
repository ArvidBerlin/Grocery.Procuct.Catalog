using Shared.Enums;

namespace Shared.Interfaces;

public interface IFileService
{
    string LoadFromFile();
    StatusCodes SaveToFile(string content);
}