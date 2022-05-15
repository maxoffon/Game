using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WMPLib;

namespace Game
{
    public class Menu : Form
    {
        private readonly string PathToSprites = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\";
        private readonly string PathToAudio = @"C:\Users\НОРД\github\Game\Game proga\Game\Sounds\";
        public WindowsMediaPlayer mixer = new WindowsMediaPlayer();
        public Menu()
        {
            mixer.URL = PathToAudio + "военная музыка1.mp3.mp3";
            mixer.settings.volume = 50;
            mixer.settings.autoStart = true;
            mixer.settings.setMode("loop", true);

            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ClientSize = new Size(800, 600);
            BackgroundImage = Image.FromFile(PathToSprites + "фон меню.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
            var buttonsOffset = 25;
            var buttonSize = new Size(170, 65);
            var initialButtonPosition = new Point(ClientSize.Width - buttonSize.Width - 65, 180);
            var buttonsHeads = new string[]
            {
                "Play.png",
                "Continue.png",
                "Options.png",
                "Exit.png"
            };
            var buttons = SetButtons(buttonsOffset, buttonSize, initialButtonPosition, buttonsHeads);
            foreach (var elem in buttons) Controls.Add(elem);
            Paint += (sender, args) =>
            {
                var g = args.Graphics;
                g.FillEllipse(Brushes.Blue, 0, 0, 100, 100);
                g.DrawImage(Image.FromFile(PathToSprites + "menu.png"), 25, 25, 500, 400);
                g.DrawImage(Image.FromFile(PathToSprites + "icon.png"), 600, 35, 100, 100);
            };
        }

        private List<Button> SetButtons(int offset, Size size, Point startLocation, string[] names)
        {
            var buttons = new List<Button>();

            for (var i = 0; i < names.Length; i++)
            {
                var pathToImage = PathToSprites + names[i];
                var button = CreateButton(pathToImage, size, startLocation);
                AddMouseEffects(button);
                startLocation.Y += button.Height + offset;
                buttons.Add(button);
            }
            SetPlayButton(buttons.Where(x => (string)x.Tag == "Play.png").First());
            SetContinueButton(buttons.Where(x => (string)x.Tag == "Continue.png").First());
            SetExitButton(buttons.Where(x => (string)x.Tag == "Exit.png").First());
            //SetOpenningOptionsPanel(buttons["Options.png"], panel);
            return buttons;
        }

       /* private Panel CreateOptionPanel(Panel panel)
        {
            panel.Size = new Size(400, 400);
            panel.Location = new Point(ClientSize.Width / 2 - panel.Width / 2, ClientSize.Height / 2 - panel.Height / 2);
            panel.BackColor = Color.Transparent;
            panel.BackgroundImage = Image.FromFile(@"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\фон опций.png");
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.Hide();

            var pathBackButton = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\backbutton.png";
            var sizeBackButton = new Size(50, 50);
            var backButton = CreateButton(pathBackButton, sizeBackButton, new Point(panel.Width - sizeBackButton.Width - 20, panel.Height - sizeBackButton.Height - 20));
            AddMouseEffects(backButton);
            //SetClosingOptionsPanel(backButton, panel);
            panel.Controls.Add(backButton);

            var label = new Label()
            {
                Location = new Point(50, 40),
                Size = new Size(400, 40),
                Text = "Громкость музыки",
                Font = new Font("Times New Roman", 22, FontStyle.Bold),
            };
            panel.Controls.Add(label);

            var a = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\бегунок.png";
            var b = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\тело бегунка.png";
            var slider = new Slider(new Point(50, 100), new Size(300, 50), a, b);
            panel.Controls.Add(slider);

            var musicLabel = new Label()
            {
                Location = new Point(slider.Left, slider.Bottom + 40),
                Text = "Без музыки:",
                Size = new Size(150, 30),
                Font = new Font("Times New Roman", 18, FontStyle.Bold)
            };
            panel.Controls.Add(musicLabel);

            var muteMusicButtonPath = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\audio.png";
            var sizeMuteMusicButton = new Size(75, 75);
            var muteMusicButton = CreateButton(muteMusicButtonPath, sizeMuteMusicButton, new Point(musicLabel.Left + musicLabel.Width / 2 - sizeMuteMusicButton.Width / 2, musicLabel.Bottom + 10));
            AddMouseEffects(muteMusicButton);
            panel.Controls.Add(muteMusicButton);

            var soundLabel = new Label()
            {
                Location = new Point(musicLabel.Location.X + musicLabel.Width + 20, musicLabel.Location.Y),
                Text = "Без звуков:",
                Size = musicLabel.Size,
                Font = musicLabel.Font
            };
            panel.Controls.Add(soundLabel);


            var muteSoundButtonPath = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\sounds.png";
            var sizeMuteSoundButton = new Size(75, 75);
            var muteSoundButton = CreateButton(muteSoundButtonPath, sizeMuteSoundButton, new Point(soundLabel.Left + soundLabel.Width / 2 - sizeMuteMusicButton.Width / 2 - 15, muteMusicButton.Location.Y));
            AddMouseEffects(muteSoundButton);
            panel.Controls.Add(muteSoundButton);

            return panel;
        }*/

        //public void SetClosingOptionsPanel(Button button, Panel panel)
        //{
        //    button.Click += (sender, args) =>
        //    {
        //        panel.Hide();
        //        foreach (var item in GetControls(Controls).Where(x => x is Button))
        //        {
        //            item.Enabled = true;
        //        }
        //    };
        //}

        //public void SetOpenningOptionsPanel(Button button, Panel panel)
        //{
        //    button.Click += (sender, args) =>
        //    {
        //        panel.Show();
        //        foreach (var item in GetControls(Controls).Where(x => x is Button))
        //        {
        //            item.Enabled = false;
        //        }
        //    };
        //}

        public void SetPlayButton(Button button)
        {
            button.Click += (sender, args) =>
            {
                var first = new First(this);
                Hide();
                first.Show();
            }; 
        }

        public void SetContinueButton(Button button)
        {
            button.Enabled = false;
            var pathToImage = PathToSprites + (string)button.Tag;
            button.BackgroundImage = Image.FromFile(pathToImage.Substring(0, pathToImage.Length - 4) + "Pressed.png");
        }

        public void SetExitButton(Button button)
        {
            button.Click += (sender, args) => Application.Exit();
        }

        public void AddMouseEffects(Button button)
        {
            var pathToImage = PathToSprites + (string)button.Tag;
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

        public Button CreateButton(string imagePath, Size size, Point location)
        {
            return new Button()
            {
                Location = location,
                Size = size,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                BackgroundImage = Image.FromFile(imagePath),
                BackgroundImageLayout = ImageLayout.Stretch,
                Tag = imagePath.Remove(0, PathToSprites.Length),
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
