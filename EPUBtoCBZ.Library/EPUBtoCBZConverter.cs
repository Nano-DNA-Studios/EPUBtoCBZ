using System.IO.Compression;

namespace EPUBtoCBZ.Library
{
    public class EPUBtoCBZConverter
    {
        private readonly string[] _FileExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        private EPUB EPUBFile { get; set; }

        public string CBZFileName { get; set; }

        public const string EXTENSION = ".cbz";

        public EPUBtoCBZConverter(string epubFilePath, string cbzFileName = "")
        {
            EPUBFile = new EPUB(epubFilePath);

            if (string.IsNullOrEmpty(cbzFileName))
                CBZFileName = EPUBFile.FileName + EXTENSION;
            else
                CBZFileName = cbzFileName;
        }

        private bool MatchingExtension (ZipArchiveEntry file)
        {
            return _FileExtensions.Any(ext => file.FullName.ToLower().EndsWith(ext));
        }

        public void Convert(string saveDirectory)
        {
            if (!Directory.Exists(saveDirectory)) 
                Directory.CreateDirectory(saveDirectory);

            string cbzPath = Path.Combine(saveDirectory, CBZFileName);

            using (FileStream epubStream = EPUBFile.OpenStream())
            using (ZipArchive epubArchive = new ZipArchive(epubStream, ZipArchiveMode.Read))
            using (FileStream cbzStream = new FileStream(cbzPath, FileMode.Create))
            using (ZipArchive cbzArchive = new ZipArchive(cbzStream, ZipArchiveMode.Create))
            {
                ZipArchiveEntry[] images = epubArchive.Entries.Where(e => MatchingExtension(e)).OrderBy(e => e.FullName).ToArray();

                int pageIndex = 0;
                foreach (ZipArchiveEntry image in images)
                {
                    WriteEntry(image, cbzArchive, pageIndex);
                    pageIndex++;
                }
            }
        }

        private void WriteEntry(ZipArchiveEntry entry, ZipArchive target, int index)
        {
            string extension = Path.GetExtension(entry.Name);
            string newEntryName = $"page_{index:D3}{extension}";

            ZipArchiveEntry newEntry = target.CreateEntry(newEntryName, CompressionLevel.Optimal);

            using (Stream sourceStream = entry.Open())
            using (Stream targetStream = newEntry.Open())
            {
                sourceStream.CopyTo(targetStream);
            }
        }
    }
}
