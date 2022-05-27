using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
using System.Media;
using System.Threading.Tasks;
using System.Threading;

namespace Game
{
    class FirstLevel : ILevel
    {
        public Menu MainForm { get; }
        public FirstLevel(Menu form)
        {
            MainForm = form;
        }
        async public void Initialize()
        {
            var preview = new Preview(MainForm);
            await MakeTask(() => preview.Initialize());
            InitGame();
        }

        public Task MakeTask(Action method)
        {
            var task = new Task(method);
            task.Start();
            return task;
        }

        public void Refresh() => MainForm.BeginInvoke(new Action(() => MainForm.Refresh()));

        public void InitGame()
        {
            MainForm.ClearInvocationList("Paint");
            MainForm.ClearInvocationList("Click");
            Refresh();
            var minutes = 1;
            var seconds = 30;
            MainForm.Paint += (sender, args) =>
            {
                args.Graphics.DrawString(minutes + ":" + seconds, new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Black, new Point(720, 10));
            };
            var timer = new System.Windows.Forms.Timer() { Interval = 1000 };
            timer.Tick += (sender, args) =>
            {
                Refresh();
                if (minutes == 0 && seconds == 0) timer.Stop();
                else if (seconds == 0)
                {
                    minutes--;
                    seconds = 59;
                }
                else seconds--;
            };
            timer.Start();
            Refresh();
        }

        public void InitPlanes(Queue<Plane> planes, int count)
        {
            var imagePlanes = new Dictionary<int, Image[]>()
            {
                {0, new Image[] { Image.FromFile(Menu.pathToSprites + "самолёт.png"), Image.FromFile(Menu.pathToSprites + "самолёт2.png") } },
                {1, new Image[] { Image.FromFile(Menu.pathToSprites + "самолёт3.png"), Image.FromFile(Menu.pathToSprites + "самолёт4.png") } }
            };
            var random = new Random();
            for(var i = 0; i < count; i++)
            {
                var randDirrection = random.Next(2);
                var randImage = random.Next(2);
                var size = new Size(300, 100);
                var location = randDirrection == 0 ? new Point(-size.Width, 0) : new Point(MainForm.Width, 0);
                var image = imagePlanes[randImage][randImage];
                var speed = randDirrection == 0 ? 30 : -30;
                var tempPlane = new Plane(location, size, speed, image);
                planes.Enqueue(tempPlane);
            }
        }
    }
}
