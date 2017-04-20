using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
            Archive.ExtractArchive(archiveLocation, tempLocation);
            Console.WriteLine("Done");
            var xml = File.ReadAllText(tempLocation + "/imsmanifest.xml");
            XElement manifest = XElement.Parse(xml);

            
            List<XElement> xele = ManifestParser.GetOrganizationElements(manifest);
            List<XElement> xres = ManifestParser.GetResourceElements(manifest);

            List<CourseContent> course = new List<CourseContent>();
            foreach (XElement x in xele)
            {
                Console.WriteLine(x);
                CourseContent cc = new CourseContent(x, tempLocation);

                if (cc.Name == "Announcements")
                {
                    foreach (XElement res in  xres.Where(f => f.Attribute("type").Value == "resource/x-bb-announcement"))
                    {
                        string path = tempLocation + "/" + res.Attribute("identifier").Value + ".dat";
                        cc.Resources.Add(new AnnouncementResource(path));
                    }
                }

                course.Add(cc);
            }
            Output.CreateRootIndex(course, extractDestination);
            Directory.Delete(tempLocation, true);
            System.Console.ReadKey();
        }
    }
}
