using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS411Crystal
{
    class GenericContent : CourseContent
    {
        public string InnerText { get; set; }
        private List<IBlackBoardResource> resources = new List<IBlackBoardResource>();

        public List<IBlackBoardResource> Resources
        {
            get { return resources; }
        }
    }
}
