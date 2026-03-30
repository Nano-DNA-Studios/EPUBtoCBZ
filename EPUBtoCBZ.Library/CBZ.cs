using System.IO.Compression;

namespace EPUBtoCBZ.Library
{
    public class CBZ : BookFormat
    {
        public const string EXTENSION = ".cbz";

        private readonly string[] _FileExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public CBZ(string filePath) : base(filePath)
        {
        }

        private bool MatchingExtension(ZipArchiveEntry file)
        {
            return _FileExtensions.Any(ext => file.FullName.ToLower().EndsWith(ext));
        }

        public override bool IsValid()
        {
            if (FilePath == null)
                return false;

            if (!File.Exists(FilePath))
                return false;

            using (ZipArchive zip = ZipFile.OpenRead(FilePath))
            {
                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    if (!MatchingExtension(entry))
                        return false;
                }
            }

            return true;
        }
    }
}
