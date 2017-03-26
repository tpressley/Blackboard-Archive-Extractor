using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchiveExtractorBusinessCode
{
    public class Output
    {
        public string targetDir { get; set; }
        
        public Output(string targetDir)
        {
            this.targetDir = targetDir;
        }

        public bool createDir()
        {
            try
            {
                if (Directory.Exists(targetDir))
                {
                    return false;
                }
                DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(targetDir);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return false;
            }
            return true;
        }
        public bool createDir(string targetDir)
        {
            this.targetDir = targetDir;
            try
            {
                this.createDir();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return false;
            }
            return true;
        }
    }
}
