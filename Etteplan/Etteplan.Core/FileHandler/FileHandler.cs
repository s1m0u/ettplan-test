using Etteplan.Core.FileHandler.Abstractions;

namespace Etteplan.Core.FileHandler
{
    public class FileHandler : IFileHandler
    {
        public string ReadFromFile(string filePath)
            => File.ReadAllText(filePath);

        public void WriteToFile(string content, string filePath)
            => File.WriteAllText(filePath, content);
    }
}
