using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Plane : Control
    {
        public readonly int Speed;

        public Plane(Point location, Size size, int speed, Image image)
        {
            Location = location;
            Size = size;
            Speed = speed;
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void Launch(Menu form)
        {
            var timerPaint = new System.Windows.Forms.Timer() { Interval = 50 };
            form.Paint += (sender, args) => args.Graphics.DrawImage(BackgroundImage, Location.X, Location.Y, Size.Width, Size.Height);
            timerPaint.Tick += (sender, args) =>
            {
                if (Location.X < -Size.Width || Location.X > form.Width) timerPaint.Stop();
                form.Invalidate();
                Location = new Point(Location.X + Speed, Location.Y);
            };
            timerPaint.Start();
        }

        public static List<Plane> InitPlanes(Menu form, int count)
        {
            var planes = new List<Plane>();
            var imagePlanes = new Dictionary<int, Image[]>()
            {
                {0, new Image[] { Image.FromFile(Menu.pathToSprites + "самолёт.png"), Image.FromFile(Menu.pathToSprites + "самолёт2.png") } },
                {1, new Image[] { Image.FromFile(Menu.pathToSprites + "самолёт3.png"), Image.FromFile(Menu.pathToSprites + "самолёт4.png") } }
            };
            var random = new Random();
            for (var i = 0; i < count; i++)
            {
                var randDirrection = random.Next(2);
                var randImage = random.Next(2);
                var size = new Size(200, 75);
                var location = randDirrection == 0 ? new Point(-size.Width, 20) : new Point(form.ClientSize.Width, 20);
                var image = imagePlanes[randDirrection][randImage];
                var speed = randDirrection == 0 ? 30 : -30;
                var tempPlane = new Plane(location, size, speed, image);
                planes.Add(tempPlane);
            }
            return planes;
        }
    }
}
