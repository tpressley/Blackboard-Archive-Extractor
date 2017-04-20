using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using ArchiveExtractorBusinessCode;

namespace CS411Crystal
{
    public partial class BlackboardExtractorMain : Form
    {
        public BlackboardExtractorMain()
        {
            InitializeComponent();
        }

        private void btnExtract_Click(object sender, System.EventArgs e)
        {
            var archiveLocation = "";
            var extractDestination = "";
            var tempLocation = "";

            archiveLocation = tbxSourcePath.Text;
            extractDestination = tbxDestination.Text;
            tempLocation = extractDestination + @"/Archive";

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
            List<CourseContent> course = new List<CourseContent>();
            foreach (XElement x in xele)
            {
                Console.WriteLine(x);
                CourseContent cc = new CourseContent(x, tempLocation);
                course.Add(cc);
            }
            Output.CreateRootIndex(course, extractDestination);
            Directory.Delete(tempLocation, true);
            System.Console.ReadKey();
        }
    }
}
