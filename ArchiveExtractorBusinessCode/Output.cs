using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
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
                //Don't want to flood existing directory, or do we?
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

        public bool CreateIndex(List<CourseContent> elements)
        {
            //var for elements in the table
            string tableContent = "";

            foreach (CourseContent obj in elements)
            {
                tableContent += "<tr><td>" + obj + "</td><td>" + obj.GetType() + "</td></tr>\n";
            }

            //Grab index template
            string indexString = "";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "CS411Crystal.ArchiveExtractorBusinessCode.StaticFiles.index.html";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                indexString = reader.ReadToEnd();
            }

            //Replace templating portions with variables retrieved
            indexString = indexString.Replace("{INDEX_TITLE}", "BAE Index File");
            indexString = indexString.Replace("{INDEX_TABLE_CONTENT}", tableContent);

            try
            {
                StreamWriter file = new StreamWriter(TargetDir + "\\index.html");
                file.WriteLine(indexString);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }

            return true;
        }

        public static bool CreateIndex(List<XElement> elements, string targetDirectory)
        {
            //var for elements in the table
            string tableContent = "";

            foreach (XElement xElement in elements)
            {
                List<XElement> children = xElement.Descendants("title").ToList();
                if (children.Count > 0)
                {
                    string title = children[0].Value;
                    tableContent += "<h2>" + title + "</h2><br />";
                }
            }

            //Grab index template
            string indexString = "";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "CS411Crystal.ArchiveExtractorBusinessCode.StaticFiles.index.html";

            //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            //{
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        indexString = reader.ReadToEnd();
            //    }
            //}

            ////Replace templating portions with variables retrieved
            //indexString = indexString.Replace("{INDEX_TITLE}", "BAE Index File");
            //indexString = indexString.Replace("{INDEX_TABLE_CONTENT}", tableContent);

            //try
            //{
            //    StreamWriter file = new StreamWriter(targetDirectory + "\\index.html");
            //    file.WriteLine(indexString);
            //    file.Close();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error: " + e);
            //    return false;
            //}
            System.IO.File.AppendAllText(targetDirectory + @"/index.html", tableContent);
            return true;
        }
    }
}
