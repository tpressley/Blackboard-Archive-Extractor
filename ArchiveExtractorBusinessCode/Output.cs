using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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
                DirectoryInfo dirInfo = Directory.CreateDirectory(TargetDir);
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

        public bool CreateRootIndex(List<CourseContent> elements)
        {
            //var for elements in the table
            string tableContent = "";

            foreach (CourseContent obj in elements)
            {
                tableContent += "<tr><td>" + obj + "</td><td>" + obj.GetType() + "</td></tr>\n";
            }

            //Grab index template
            string indexString = "";
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "CS411Crystal.ArchiveExtractorBusinessCode.StaticFiles.index.html";

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

        public static bool CreateRootIndex(List<CourseContent> content, string targetDirectory)
        {
            Directory.CreateDirectory(targetDirectory);
            //var for elements in the table
            string pageHtml = "";

            foreach (CourseContent element in content)
            {
                pageHtml += "<a href='" + element.RefId + @".html'>" + element.Name + "</a><br />";
                CreateResourceHtml(element.Children, element.Resources, targetDirectory + @"/" + element.RefId + @".html");
            }

            //Grab index template
            string indexString = "";
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "CS411Crystal.ArchiveExtractorBusinessCode.StaticFiles.index.html";

            
            System.IO.File.AppendAllText(targetDirectory + @"/index.html", pageHtml);
            return true;
        }

        public static bool CreateResourceHtml(List<CourseContent> content, List<BlackBoardResource> resources, string targetPath)
        {
            string pageHtml = "<html>";
            foreach (CourseContent pageContent in content)
            {
                pageHtml += "<a href='" + pageContent.RefId + @".html'>" + pageContent.Name + "</a><br />";
                CreateResourceHtml(pageContent.Children, pageContent.Resources, Path.GetDirectoryName(targetPath) + @"/" + pageContent.RefId + @".html");
                //foreach (BlackBoardResource res in pageContent.Resources)
                //{
                //    pageHtml += res.Text;
                //}
            }
            foreach (BlackBoardResource res in resources)
            {
                pageHtml += res.Text;
                pageHtml += "<hr>";
            }
            pageHtml += "</html>";
            System.IO.File.AppendAllText(targetPath, pageHtml);
            return true;
        }
    }
}
