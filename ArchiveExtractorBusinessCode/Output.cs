using System;
using System.IO;
using System.Xml.Linq;
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
        public bool CreateIndex(System.Collections.Generic.List<Object> elements)
        {
            //var for elements in the table
            string tableContent = "";
            
            foreach(Object obj in elements)
            {
                tableContent += "<tr><td>" + obj.ToString() + "</td><td>" + obj.GetType() + "</td></tr>\n";
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
                System.IO.StreamWriter file = new System.IO.StreamWriter(TargetDir + "\\index.html");
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
    }
}
