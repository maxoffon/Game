using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Bomb : Control
    {
        public static readonly int Speed = 20;
        public Bomb(Point location, Size size, Image background)
        {
            Location = location;
            Size = size;
            BackgroundImage = background;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void Drop(Menu form)
        {
            PaintEventHandler lambda = (sender, args) => args.Graphics.DrawImage(BackgroundImage, Location.X, Location.Y, Size.Width, Size.Height);
            form.Paint += lambda;
            var timer = new Timer() { Interval = 50 };
            timer.Tick += (sender, args) =>
            {
                if (Location.Y >= form.ClientSize.Height) { timer.Stop(); form.Paint -= lambda; }
                else
                {
                    Location = new Point(Location.X, Location.Y + Speed);
                    form.Refresh();
                }
            };
            timer.Start();
        }
    }
}
