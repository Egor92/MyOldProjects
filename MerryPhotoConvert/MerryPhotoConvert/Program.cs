using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MerryPhotoConvert
{
    class Program
    {
        const string SourceFileName = "source.txt";
        const string PhotoSourceDirectoryPath = @"E:\Egor\Медиа\Свадьба\Егор и Галина 14 июля\";
        const string PhotoTargetDirectoryPath = @".\Photo\";
        private static readonly IList<string> Prefixes = new[] { "ELM", "FLM" };

        static void Main(string[] args)
        {
            Console.WriteLine("Choose the command:");
            Console.WriteLine("1. Copy photos");
            Console.WriteLine("2. Create txt file");

            int commandIndex;
            bool parseResult;
            do
            {
                var str = Console.ReadLine();
                parseResult = !int.TryParse(str, out commandIndex);
            } while (parseResult);

            switch (commandIndex)
            {
                case 1:
                    CopyPhotos();
                    break;
                case 2:
                    break;
            }

            Console.WriteLine("Press any key to exit....");
            Console.ReadKey();
        }

        private static void CopyPhotos()
        {
            if (!File.Exists(SourceFileName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} doesn't exist", SourceFileName);
                Console.ResetColor();
                return;
            }

            var lines = File.ReadAllLines("source.txt");

            if (Directory.Exists(PhotoTargetDirectoryPath))
            {
                Console.WriteLine("Target directory deleting....");
                foreach (var filePath in Directory.EnumerateFiles(PhotoTargetDirectoryPath))
                    File.Delete(filePath);
                Console.WriteLine("Target directory was deleted");
            }
            else
            {
                Directory.CreateDirectory(PhotoTargetDirectoryPath);
            }

            string prefix = string.Empty;
            int copiedFileCount = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (Prefixes.Contains(line))
                {
                    prefix = line;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                const string tilde = "~";
                if (line.StartsWith(tilde) || line.EndsWith(tilde))
                {
                    line = line.Replace(tilde, string.Empty);
                }

                var photoFileName = string.Format("{0}_{1}.jpg", prefix, line);
                var sourcePath = Path.Combine(PhotoSourceDirectoryPath, photoFileName);
                var targetPath = Path.Combine(PhotoTargetDirectoryPath, photoFileName);

                if (!File.Exists(sourcePath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} does not exist in source folder", photoFileName);
                    Console.ResetColor();
                    continue;
                }

                File.Copy(sourcePath, targetPath);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(photoFileName);
                Console.ResetColor();

                copiedFileCount++;
            }

            Console.WriteLine("Copying finished");
            Console.WriteLine("Total {0} files copied", copiedFileCount);
        }
    }
}
