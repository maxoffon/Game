using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class House : Control
    {
        public House(Point location, Size size, Image background)
        {
            Location = location;
            Size = size;
            BackgroundImage = background;
            BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
