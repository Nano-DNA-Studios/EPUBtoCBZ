using EPUBtoCBZ.Library;

namespace EPUBtoCBZ.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("File not Specified");
                return;
            }

            string filePath = args[0];
            string outputPath = "";
            EPUBtoCBZConverter converter;

            Console.Write($"Converting {Path.GetFileName(filePath)} to CBZ Format...  ");

            if (args.Length >= 2)
            {
                outputPath = args[1];

                string outputFileName = Path.GetFileName(outputPath);
                string? outputFilePath = Path.GetDirectoryName(outputPath);

                converter = new EPUBtoCBZConverter(filePath, outputFileName);

                if (outputFilePath == null)
                {
                    Console.WriteLine("Invalid Output File Path");
                    return;
                }

                converter.Convert(outputFilePath);

                Console.WriteLine("File Converted!");

                return;
            }

            converter = new EPUBtoCBZConverter(filePath);

            converter.Convert("./");

            Console.WriteLine("File Converted!");
        }
    }
}
