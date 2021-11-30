using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace nvdi
{
    class Program
    {
        static void Main(string[] args)
        {
            var redFile = args.Length > 0 ? args[0] : PromptForFilePath("Please enter the path to the red file:");
            var nirFile = args.Length > 1 ? args[1] : PromptForFilePath("Please enter the path to the nir file:");
            var nvdiGenerator = new NvdiGenerator(redFile, nirFile);
            SaveNvdi(redFile, nvdiGenerator.Generate());
        }

        private static void SaveNvdi(string redFile, Image<Rgba32> nvdiImage)
        {
            var nvdiFileName = Path.Combine(Path.GetDirectoryName(redFile), Path.GetFileNameWithoutExtension(redFile) + "_nvdi.bmp");
            nvdiImage.Save(nvdiFileName);
            Process.Start("explorer.exe", nvdiFileName);
        }

        private static string PromptForFilePath(string message)
        {
            var file = string.Empty;
            while (string.IsNullOrWhiteSpace(file))
            {
                Console.WriteLine(message);
                file = Console.ReadLine();
                if (!File.Exists(file))
                {
                    Console.WriteLine("File does not exist.");
                    file = string.Empty;
                }
            }
            return file;
        }
    }
}
