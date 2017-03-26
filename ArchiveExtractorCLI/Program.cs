using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveExtractorBusinessCode;

namespace ArchiveExtractorCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string ArchiveLocation = "";
            string ExtractDestination = "";

            try
            {
                ArchiveLocation = args[0];
                ExtractDestination = args[1];
            }
            catch (IndexOutOfRangeException ex)
            {
                System.Console.WriteLine("Usage: ArchiveExtractor 'ArchiveToExtract' 'TargetLocation'");
            }
        }
    }
}
