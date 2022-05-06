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
                AddMouseEffects(button);
                buttons[buttonsHeads[i]] = button;
                initialButtonPosition.Y += button.Height + buttonsOffset;
            }

            var panel = new Panel();
            SetOptionPanel(panel);
            Controls.Add(panel);

            SetPlayButton(buttons["Play.png"]);
            SetContinueButton(buttons["Continue.png"]);
            SetExitButton(buttons["Exit.png"]);
            SetOpenningOptionsPanel(buttons["Options.png"], panel);

            foreach (var elem in buttons.Values)
                Controls.Add(elem);
            Controls.Add(title);
            Controls.Add(icon);
        }

        public void SetOptionPanel(Panel panel)
        {
            panel.Size = new Size(400, 400);
            panel.Location = new Point(ClientSize.Width / 2 - panel.Width / 2, ClientSize.Height / 2 - panel.Height / 2);
            panel.BackColor = Color.Transparent;
            panel.BackgroundImage = Image.FromFile(@"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\фон опций.jpg");
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.Hide();

            var pathBackButton = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\backbutton.png";
            var sizeBackButton = new Size(50, 50);
            var backButton = CreateButton(pathBackButton, sizeBackButton, new Point(panel.Width - sizeBackButton.Width - 20, panel.Height - sizeBackButton.Height - 20));
            AddMouseEffects(backButton);
            SetClosingOptionsPanel(backButton, panel);
            panel.Controls.Add(backButton);

            var a = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\бегунок.png";
            var b = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\тело бегунка.png";
            var slider = new Slider(new Point(50, 100), new Size(300, 50), a, b);
            panel.Controls.Add(slider);

            var label = new Label()
            {
                Location = new Point(50, 50),
                Size = new Size(400, 40),
                Text = "Громкость музыки",
                Font = new Font("Times New Roman", 22, FontStyle.Bold),
            };
            panel.Controls.Add(label);
        }

        public List<Control> GetControls(Control.ControlCollection controls)
        {
            var result = new List<Control>();
            foreach (var item in controls)
                result.Add((Control)item);
            return result;
        }

        public void SetClosingOptionsPanel(Button button, Panel panel)
        {
            button.Click += (sender, args) =>
            {
                panel.Hide();
                foreach (var item in GetControls(Controls).Where(x => x is Button))
                {
                    item.Enabled = true;
                }
            };
        }

        public void SetOpenningOptionsPanel(Button button, Panel panel)
        {
            button.Click += (sender, args) =>
            {
                panel.Show();
                foreach (var item in GetControls(Controls).Where(x => x is Button))
                {
                    item.Enabled = false;
                }
            };
        }

        public void SetPlayButton(Button button)
        {
            button.Click += (sender, args) =>
            {
                var form = new First(this);
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

        public static void AddMouseEffects(Button button)
        {
            var pathToImage = button.Tag.ToString();
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
