using System;
using System.IO;

namespace ArchiveExtractorBusinessCode
{
    public class Output
    {
        public string TargetDir { get; set; }
        
        public Output(string targetDir)
        {
            TargetDir = targetDir;
        }

        public bool CreateDir()
        {
            try
            {
                if (Directory.Exists(TargetDir))
                {
                    return false;
                }
                var dirInfo = Directory.CreateDirectory(TargetDir);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }
        public bool CreateDir(string targetDir)
        {
            TargetDir = targetDir;
            try
            {
                CreateDir();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }
    }
}
