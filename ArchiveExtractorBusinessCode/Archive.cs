using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace ArchiveExtractorBusinessCode
{
    public class Archive
    {
        public static void ExtractArchive(string pathToArchive, string pathToExtract)
        {
            ZipFile.ExtractToDirectory(pathToArchive,pathToExtract);
        }
    }
}