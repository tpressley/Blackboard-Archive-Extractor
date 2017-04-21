using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using ArchiveExtractorBusinessCode;

namespace ArchiveExtractorCLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var archiveLocation = "";
            var extractDestination = "";
            var tempLocation = "";

            try
            {
                archiveLocation = args[0];
                extractDestination = args[1];
                tempLocation = extractDestination + @"/Archive";
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Usage: ArchiveExtractor 'ArchiveToExtract' 'TargetLocation'");
                return;
            }
            if (Directory.Exists(tempLocation))
            {
                Directory.Delete(tempLocation, true);
            }
            
            Console.WriteLine("Extracting Zip...");
            try
            {

                int count = 1;
                string fileNameOnly = Path.GetFileNameWithoutExtension(tempLocation);
                string extension = Path.GetExtension(tempLocation);
                string path = Path.GetDirectoryName(tempLocation);
                string newFullPath = tempLocation;

                while (File.Exists(tempLocation))
                {
                    Console.WriteLine(count);
                    string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                    tempLocation = Path.Combine(path, tempFileName + extension);
                }
                Archive.ExtractArchive(archiveLocation, tempLocation);
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Usage: ArchiveExtractor 'ArchiveToExtract' 'TargetLocation'");
                Console.WriteLine("Error BAE001: File Not found");
                return;
            }

            var xml = File.ReadAllText(tempLocation + "/imsmanifest.xml");
            XElement manifest = XElement.Parse(xml);

            
            List<XElement> xele = ManifestParser.GetOrganizationElements(manifest);
            List<CourseContent> course = new List<CourseContent>();
            foreach (XElement x in xele)
            {
                //Console.WriteLine(x);
                CourseContent cc = new CourseContent(x, tempLocation);
                course.Add(cc);
            }
            Output.CreateRootIndex(course, extractDestination);
            Directory.Delete(tempLocation, true);
            System.Console.ReadKey();
        }
    }
}
