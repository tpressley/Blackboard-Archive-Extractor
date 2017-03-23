using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CS411Crystal
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
