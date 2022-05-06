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
    public partial class First : Form
    {
        public First(Form menu)
        {
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ClientSize = new Size(800, 600);
            BackgroundImage = Image.FromFile(@"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\фон2.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;

            var planeCount = 10;
            var random = new Random();
            var timer = new Timer();
            timer.Interval = 2000;
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                if (planeCount == 0) timer.Stop();
                var a = new Plane(this, Image.FromFile(@"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\самолёт.png"));
                a.Start();
                Controls.Add(a);
                planeCount--;
            };
            

            var b = new Button()
            {
                Location = new Point(Left + 100, Bottom - 100)
            };
            b.Click += (sender, args) =>
            {
                Hide();
                menu.Show();
            };
            Controls.Add(b);
            
        }
    }  
}
