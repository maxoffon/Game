using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Game
{
    public partial class Menu : Form
    {
        public Menu()
        {
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ClientSize = new Size(800, 600);
            BackgroundImage = Image.FromFile(@"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\фон меню.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;

            var title = CreatePicture(@"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\menu.png", new Size(500, 400), new Point(25, 25));
            var icon = CreatePicture(@"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\icon.png", new Size(100, 100), new Point(600, title.Top + 10));

            var buttonPath = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\";
            var buttonsOffset = 25;
            var buttonSize = new Size(170, 65);
            var initialButtonPosition = new Point(ClientSize.Width - buttonSize.Width - 65, 80 + icon.Height);
            var buttonsHeads = new string[]
            {
                "Play.png",
                "Continue.png",
                "Options.png",
                "Exit.png"
            };
            var buttons = new Dictionary<string, Button>();
            for (var i = 0; i < buttonsHeads.Length; i++)
            {
                var pathToImage = buttonPath + buttonsHeads[i];
                var button = CreateButton(pathToImage, buttonSize, initialButtonPosition);
                AddMouseEffects(button, pathToImage);
                buttons[buttonsHeads[i]] = button;
                initialButtonPosition.Y += button.Height + buttonsOffset;
            }
            SetPlayButton(buttons["Play.png"]);
            SetContinueButton(buttons["Continue.png"]);
            SetExitButton(buttons["Exit.png"]);
            foreach (var elem in buttons.Values)
                Controls.Add(elem);
            Controls.Add(title);
            Controls.Add(icon);
        }
        public void SetPlayButton(Button button)
        {
            button.Click += (sender, args) =>
            {
                var form = new First();
                Hide();
                form.Show();
            };
        }

        public void SetContinueButton(Button button)
        {
            button.Enabled = false;
            var pathToImage = (string)button.Tag;
            button.BackgroundImage = Image.FromFile(pathToImage.Substring(0, pathToImage.Length - 4) + "Pressed.png");
        }

        public void SetExitButton(Button button)
        {
            button.Click += (sender, args) => Application.Exit();
        }

        public static void AddMouseEffects(Button button, string pathToImage)
        {
            var pathToPressedImage = pathToImage.Substring(0, pathToImage.Length - 4) + "Pressed.png";
            button.MouseDown += (sender, args) =>
            {
                button.BackgroundImage = Image.FromFile(pathToPressedImage);
            };
            button.MouseUp += (sender, args) =>
            {
                button.BackgroundImage = Image.FromFile(pathToImage);
            };
        }

        public static PictureBox CreatePicture(string imagePath, Size size, Point location)
        {
            return new PictureBox()
            {
                Location = location,
                Size = size,
                BackColor = Color.Transparent,
                BackgroundImage = Image.FromFile(imagePath),
                BackgroundImageLayout = ImageLayout.Stretch,
                Tag = imagePath
            };
        }

        public static Button CreateButton(string imagePath, Size size, Point location)
        {
            return new Button()
            {
                Location = location,
                Size = size,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                BackgroundImage = Image.FromFile(imagePath),
                BackgroundImageLayout = ImageLayout.Stretch,
                Tag = imagePath,
                FlatAppearance =
                {
                    MouseDownBackColor = Color.Transparent,
                    MouseOverBackColor = Color.Transparent,
                    BorderSize = 0
                }
            };
        }
    }
}
