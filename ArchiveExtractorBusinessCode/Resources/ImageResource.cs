using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ArchiveExtractorBusinessCode
{
    class ImageResource : IBlackBoardResource
    {
        private byte[] pictureByteStream;
        private Image picture;

        public Image Picture
        {
            get { return picture; }
            set { picture = value; }
        }
    }
}
