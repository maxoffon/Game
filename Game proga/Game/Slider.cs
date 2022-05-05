using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace Game
{
    public class Slider : Control
    {
        public PictureBox Handler { get; }
        public double Value { get; set; } = 0;
        public Slider(Point location, Size size, string backgroundImage, string handlerImage)
        {
            Location = location;
            Size = size;
            BackgroundImage = Image.FromFile(backgroundImage);
            BackgroundImageLayout = ImageLayout.Stretch;
            var handlerSize = new Size(size.Height, size.Height);
            var handlerLocation = Point.Empty;
            Handler = Menu.CreatePicture(handlerImage, handlerSize, handlerLocation);
            Controls.Add(Handler);
        }
    }
}
