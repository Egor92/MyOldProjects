using System.IO;

namespace VoicesCalculatorApplication
{
    public class FileWriter : IWriter
    {
        private readonly string _filePath;

        public FileWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteLine(string text)
        {
            using (var streamWriter = new StreamWriter(_filePath, true))
            {
                streamWriter.WriteLine(text);
            }
        }
    }
}
