using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveExtractorBusinessCode
{
    class CourseContent
    {
        public string RefId { get; set; }
        public string Name { get; set; }
        public CourseContent Parent { get; set; }
        private List<CourseContent> children = new List<CourseContent>();
        public List<CourseContent> Children {  get { return children; }  } 

        public CourseContent()
        {
            //Generic Constructor to make the compiler happy
            RefId = "";
            Name = "";
            Parent = null;
        }

        public CourseContent(CourseContent parent, string refId, string name)
        {
            this.Parent = parent;
            this.RefId = refId;
            this.Name = name;
        }

        public CourseContent(string refId, string name)
        {
            this.RefId = refId;
            this.Name = name;
        }
    }
}
