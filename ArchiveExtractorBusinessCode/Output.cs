﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ArchiveExtractorBusinessCode.Resources;

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

        public static bool CreateRootIndex(List<CourseContent> content, string targetDirectory)
        {
            Directory.CreateDirectory(targetDirectory);
            //var for elements in the table
            string pageHtml = "";

            foreach (CourseContent element in content)
            {
                pageHtml += "<a href='" + element.RefId + @".html'>" + element.Name + "</a><br />";
                CreateResourceHtml(element.Children,targetDirectory + @"/" + element.RefId + @".html");
            }

            //Grab index template
            string indexString = "";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "CS411Crystal.ArchiveExtractorBusinessCode.StaticFiles.index.html";

            
            System.IO.File.AppendAllText(targetDirectory + @"/index.html", pageHtml);
            return true;
        }

        public static bool CreateResourceHtml(List<CourseContent> content, string targetPath)
        {
            string pageHtml = "<html>";
            foreach (CourseContent pageContent in content)
            {
                pageHtml += "<a href='" + pageContent.RefId + @".html'>" + pageContent.Name + "</a><br />";
                CreateResourceHtml(pageContent.Children, Path.GetDirectoryName(targetPath) + @"/" + pageContent.RefId + @".html");
                foreach (TextResource res in pageContent.Resources)
                {
                    pageHtml += res.Text;
                }
            }
            
            pageHtml += "</html>";
            System.IO.File.AppendAllText(targetPath, pageHtml);
            return true;
        }
    }
}
