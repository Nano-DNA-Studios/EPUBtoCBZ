using System.IO.Compression;

namespace EPUBtoCBZ.Library
{
    public abstract class BookFormat
    {
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public BookFormat(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileNameWithoutExtension(filePath);
        }

        public abstract bool IsValid();

        public FileStream OpenStream()
        {
            if (FilePath == null)
                throw new FileNotFoundException("FilePath not Specified");

            if (!File.Exists(FilePath))
                throw new FileNotFoundException("File doesn't exist");

            return new FileStream(FilePath, FileMode.Open);
        }

        public void Open(string outputPath)
        {
            if (outputPath == null)
            {
                Console.WriteLine("Path not specified");
                return;
            }

            if (!Directory.Exists(outputPath))
            {
                Console.WriteLine("File doesn't Exist");
                return;
            }

            using (FileStream stream = OpenStream())
            {
                using (ZipArchive archive = new ZipArchive(stream))
                {
                    archive.ExtractToDirectory(outputPath);
                }
            }
        }
    }
}
