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
        public First()
        {
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ClientSize = new Size(800, 600);
            BackgroundImage = Image.FromFile(@"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\фон2.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
            var a = new Planes(this);
            a.Main();

        }
    }

    public class Planes
    {
        int planeNumber = 10;
        public Form Form = new First();
        public Planes(Form form)
        {
            Form = form;
        }
        public void Main()
        {
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (sender, args) =>
            {
                MovePlane();
                planeNumber--;
                if (planeNumber == 0) return;
            };
            timer.Start();
        }
        public void MovePlane()
        {
            var plane = new Plane();
            var picture = Menu.CreatePicture(plane.Current, new Size(100, 100), new Point(-100, 10));
            while (picture.Location.X != Form.ClientSize.Height)
            {
                picture = CreateNew(picture, plane.Speed);
                Form.Invalidate();
            }
        }

        PictureBox CreateNew(PictureBox picture, int offset)
        {
            var newLocation = new Point(picture.Location.X + offset, picture.Location.Y);
            return Menu.CreatePicture((string)picture.Tag, picture.Size, newLocation);
        }
    }
}
