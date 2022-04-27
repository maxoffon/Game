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
    public partial class Form1 : Form
    {
        public Form1()
        {
            DoubleBuffered = true;
            MaximizeBox = false;
            ClientSize = new Size(1000, 600);
            BackgroundImage = Image.FromFile(@"C:\Users\НОРД\github\Game\Game proga\Game\Game\Sprites\фото1.jpg");

            var menu = new PictureBox()
            {
                Location = new Point(50, 20),
                Size = new Size(600, 450),
                BackgroundImage = Image.FromFile(@"C:\Users\НОРД\github\Game\Game proga\Game\Game\Sprites\menu.png"),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent
            };
            Controls.Add(menu);

            

            var buttons = new List<PictureBox>();
            var buttonSize = new Size(180, 76);
            var buttonsOffset = 25;
            var initialButtonLocation = new Point(ClientSize.Width - buttonSize.Width - 85, 180);
            var pathToButtons = @"C:\Users\НОРД\github\Game\Game proga\Game\Game\Sprites\";
            var buttonsNames = new string[]
            {
                "Play.png",
                "Continue.png",
                "Options.png",
                "Exit.png"
            };
            for (var i = 0; i < buttonsNames.Length; i++)
            { 
                var pathToCurrentButton = pathToButtons + buttonsNames[i];
                var currentImage = Image.FromFile(pathToCurrentButton);
                var currentButton = CreateButton(buttonSize, currentImage, initialButtonLocation);
                Add_UpAndDown_MouseEffect(currentButton, pathToCurrentButton);
                buttons.Add(currentButton);
                Controls.Add(currentButton);
                initialButtonLocation.Y += buttonSize.Height + buttonsOffset;
            }
        }

        public static void Add_UpAndDown_MouseEffect(PictureBox button, string pathToButton)
        {
            button.MouseDown += (sender, args) =>
            {
                button.BackgroundImage = Image.FromFile(pathToButton.Substring(0, pathToButton.Length - 4) + "Pressed.png");
            };
            button.MouseUp += (sender, args) =>
            {
                button.BackgroundImage = Image.FromFile(pathToButton);
            };
        }

        public static PictureBox CreateButton(Size size, Image image, Point location)
        {
            return new PictureBox()
            {
                Location = location,
                Size = size,
                BackgroundImage = image,
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
            };
        }
    }
}
