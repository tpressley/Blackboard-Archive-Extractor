using System;

namespace ArchiveExtractorCLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var archiveLocation = "";
            var extractDestination = "";

            try
            {
                archiveLocation = args[0];
                extractDestination = args[1];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Usage: ArchiveExtractor 'ArchiveToExtract' 'TargetLocation'");
            }
        }
    }
}
