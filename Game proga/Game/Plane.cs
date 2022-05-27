using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Plane : Control
    {
        public readonly int speed;

        public Plane(Point location, Size size, int speedInt, Image image)
        {
            Location = location;
            Size = size;
            speed = speedInt;
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void Launch(Menu form)
        {
            Timer timer = new Timer() { Interval = 50 };
            form.Paint += (sender, args) => args.Graphics.DrawImage(BackgroundImage, Location.X, Location.Y, Size.Width, Size.Height);
            timer.Tick += (sender, args) =>
            {
                if (Location.X < -Size.Width || Location.X > form.Width) timer.Stop();
                form.Refresh();
                Location = new Point(Location.X + speed, Location.Y);
            };
            timer.Start();
        }
    }
}
