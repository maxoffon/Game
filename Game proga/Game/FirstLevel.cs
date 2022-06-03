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
        public void Initialize()
        {
            var a = new BackgroundWorker();
            var preview = new Preview(MainForm);
            a.DoWork += (sender, args) => preview.Initialize();
            a.RunWorkerCompleted += (sender, args) => InitGame();
            a.RunWorkerCompleted += (sender, args) => InitPlanes();
            a.RunWorkerAsync();
        }

        public Task MakeTask(Action method)
        {
            var task = new Task(method);
            task.Start();
            return task;
        }

        public void InitGame()
        {
            MainForm.ClearInvocationList("Paint");
            InitHouses();
            var minutes = 0;
            var seconds = 30;
            MainForm.Paint += (sender, args) =>
            {
                args.Graphics.DrawString(minutes + ":" + seconds, new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Black, new Point(720, 10));
            };
            var timer = new System.Windows.Forms.Timer() { Interval = 1000 };
            timer.Tick += (sender, args) =>
            {
                MainForm.Refresh();
                if (minutes == 0 && seconds == 0) { timer.Stop(); ShowCongrats(); MainForm.Refresh(); }
                else if (seconds == 0)
                {
                    minutes--;
                    seconds = 59;
                }
                else seconds--;
            };
            timer.Start();
            MainForm.Refresh();
        }

        public void InitHouses()
        {
            var images = new Image[]
            {
                Image.FromFile(Menu.pathToSprites + "дом.png"),
                Image.FromFile(Menu.pathToSprites + "дом3.png"),
                Image.FromFile(Menu.pathToSprites + "дом2.png")
            };
            var initLocation = new Point(10, 380);
            var size = new Size(250, 200);
            for (var i = 0; i < 3; i++)
            {
                var house = new House(initLocation, size, images[i]);
                MainForm.Paint += (sender, args) => args.Graphics.DrawImage(house.BackgroundImage, house.Location.X, house.Location.Y, house.Size.Width, house.Size.Height);
                initLocation = new Point(initLocation.X + size.Width + 15, initLocation.Y);
            }
        }

        public void InitPlanes()
        {
            var planes = Plane.InitPlanes(MainForm, 10);
            var count = 0;
            var t = new System.Windows.Forms.Timer() { Interval = 3100 };
            t.Tick += (sender, args) =>
            {
                if (count > 9) t.Stop();
                else LaunchPlane(planes[count], ref count);
            };
            LaunchPlane(planes[count], ref count);
            t.Start();
        }

        public void LaunchPlane(Plane plane, ref int count)
        {
            plane.Launch(MainForm);
            count++;
        }

        public void ShowCongrats()
        {
            MainForm.Paint += (sender, args) =>
            {
                var g = args.Graphics;
                g.DrawImage(Image.FromFile(Menu.pathToSprites + "фон опций.png"), MainForm.Width / 2 - 290, MainForm.Height / 2 - 270, 600, 520);
                g.DrawString("Поздравляю с окончанием первого уровня!", new Font("Times New Roman", 19, FontStyle.Bold), Brushes.Black, new Point(170, 140));
                g.DrawString("Нажмите Пробел для начала следующего уровня", new Font("Times New Roman", 16, FontStyle.Bold), Brushes.Black, new Point(170, 190));
            };
            MainForm.KeyPress += (sender, args) => { if (args.KeyChar == ' ') Application.Restart(); };
        }
    }
}
