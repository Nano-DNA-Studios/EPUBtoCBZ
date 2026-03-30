using System.IO.Compression;

namespace EPUBtoCBZ.Library
{
    public class EPUB : BookFormat
    {
        public const string EXTENSION = ".epub";

        public EPUB (string filePath) : base(filePath)
        {
        }

        public override bool IsValid()
        {
            if (FilePath == null)
                return false;

            if (!File.Exists(FilePath))
                return false;

            using (ZipArchive zip = ZipFile.OpenRead(FilePath))
            {
                ZipArchiveEntry? entry = zip.GetEntry("mimetype");

                return entry != null;
            }
        }
    }
}
