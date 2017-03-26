using System.Collections.Generic;

namespace ArchiveExtractorBusinessCode
{
    internal class GenericContent : CourseContent
    {
        public string InnerText { get; set; }

        public List<IBlackBoardResource> Resources { get; } = new List<IBlackBoardResource>();
    }
}
