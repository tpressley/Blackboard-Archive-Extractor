using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ArchiveExtractorBusinessCode
{
    public class ManifestParser
    {
        public static List<XElement> GetOrganizationElements(XElement manifestXml)
        {
            List<XElement> items = new List<XElement>();
            items = manifestXml.Descendants("item").ToList();
            return items;
        }

        public static List<XElement> GetResourceElements(XElement manifestXml)
        {
            List<XElement> resources = new List<XElement>();
            resources = manifestXml.Descendants("resource").ToList();
            return null;
        }
    }
}