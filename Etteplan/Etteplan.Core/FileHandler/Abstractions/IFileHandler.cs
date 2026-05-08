
namespace Etteplan.Core.FileHandler.Abstractions
{
    public interface IFileHandler
    {
        void WriteToFile(string content, string filePath);
        string ReadFromFile(string filePath);
    }
}
