using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArchiveExtractorBusinessCode
{
    public class AnnouncementResource : BlackBoardResource
    {
        public string RefId { get; set; }
        public override string Text { get; set; }
        public string PathToResourceFile { get; set; }
        public string Url { get; set; }

        public AnnouncementResource(string text, string refId)
        {
            RefId = refId;
            Text = text;
        }

        public AnnouncementResource(string pathToResourceFile)
        {
            string xml = File.ReadAllText(pathToResourceFile);
            XElement xele = XElement.Parse(xml);
            if (xele.Descendants("TEXT").Any())
            {
                List<XElement> texts = xele.Descendants("TEXT").ToList();
                this.Text = texts[0].Value;
            }
            else
            {
                this.Text = "";
            }
            this.RefId = Path.GetFileNameWithoutExtension(PathToResourceFile);
            this.PathToResourceFile = pathToResourceFile;
        }
    }
}
