using System.Collections.Generic;

namespace ArchiveExtractorBusinessCode
{
    internal class CourseContent
    {
        public string RefId { get; set; }
        public string Name { get; set; }
        public CourseContent Parent { get; set; }
        public List<CourseContent> Children { get; } = new List<CourseContent>();

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
    }
}
