using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class Plane : PictureBox
    {
        static int Speed { get; } = 20;
        Form MainForm;

        public Plane(Form form, Image image)
        {
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Color.Transparent;
            Size = new Size(174, 45);
            Location = new Point(-174, 50);
            MainForm = form;
        }

        public void Start()
        {
            var timer = new Timer();
            timer.Interval = 100;
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                if (Location.X >= MainForm.ClientSize.Width)
                {
                    timer.Stop();
                    MainForm.Controls.Remove(this);
                }
                Location = new Point(Location.X + Speed, Location.Y);
                Refresh();
            };
            
        }
    }
}
