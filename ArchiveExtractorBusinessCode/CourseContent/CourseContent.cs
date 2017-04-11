using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchiveExtractorBusinessCode
{
    public class CourseContent
    {
        public string RefId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public CourseContent Parent { get; set; }
        private List<CourseContent> children = new List<CourseContent>();
        public List<CourseContent> Children 
        {
            get { return children; }
        }

        public CourseContent()
        {
            //Generic Constructor to make the compiler happy
            RefId = "";
            Name = "";
            Parent = null;
        }

        public CourseContent(CourseContent parent, string refId, string name)
        {
            Parent = parent;
            RefId = refId;
            Name = name;
        }

        public CourseContent(string refId, string name)
        {
            RefId = refId;
            Name = name;
        }

        public CourseContent(XElement XmlItemManifestElement)
        {
            if (XmlItemManifestElement.Descendants("title").ToList().Any())
            {
                this.Name = XmlItemManifestElement.Descendants("title").ToList()[0].Value;
            }
            this.Id = XmlItemManifestElement.Attributes().ToList()[0].Value;
            this.RefId = XmlItemManifestElement.Attributes().ToList()[1].Value;

            //Recursively create child elements here
            if (XmlItemManifestElement.Descendants("item").ToList().Any())
            {
                CourseContent newChild = new CourseContent(XmlItemManifestElement.Descendants("item").ToList()[0]);
                children.Add(newChild);
            }
        }
    }
}
