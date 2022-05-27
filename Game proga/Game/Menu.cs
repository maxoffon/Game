using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using WMPLib;
using System.Media;
using System.Reflection;
using System.ComponentModel;

namespace Game
{
    public class Menu : Form
    {
        public static readonly string pathToSprites = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\";
        public static readonly string pathToAudio = @"C:\Users\НОРД\github\Game\Game proga\Game\Sounds\";
        public static readonly AudioManager audioManager = new AudioManager(new SoundPlayer(pathToAudio + "щелчок.wav"), new WindowsMediaPlayer());

        public void ClearInvocationList(string eventName)
        {
            PropertyInfo propertyInfo = GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            EventHandlerList eventHandlerList = propertyInfo.GetValue(this, new object[] { }) as EventHandlerList;
            FieldInfo fieldInfo = typeof(Control).GetField("Event" + eventName, BindingFlags.NonPublic | BindingFlags.Static);

            object eventKey = fieldInfo.GetValue(this);
            var eventHandler = eventHandlerList[eventKey];
            Delegate[] invocationList = eventHandler.GetInvocationList();
            foreach (var item in invocationList)
            {
                GetType().GetEvent(eventName).RemoveEventHandler(this, item);
            }
        }

        public Button CreateButton(string head, Point location, Size size)
        {
            var image = Image.FromFile(pathToSprites + head);
            return new Button()
            {
                BackgroundImage = image,
                BackgroundImageLayout = ImageLayout.Stretch,
                Location = location,
                Size = size,
                Name = head
            };
        }

        public void InitMixer(WindowsMediaPlayer mixer)
        {
            mixer.URL = pathToAudio + "военная музыка1.mp3";
            mixer.settings.volume = 50;
            mixer.settings.autoStart = true;
            mixer.settings.setMode("loop", true);
        }

        public void InitStartMenu()
        {
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ClientSize = new Size(800, 600);
            BackgroundImage = Image.FromFile(pathToSprites + "фон меню.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
            InitMixer(audioManager.MusicMixer);
        }

        public List<Button> InitMainButtons()
        {
            var buttons = new List<Button>();
            var heads = new string[] { "Play.png", "Continue.png", "Exit.png" };
            var initialPosition = new Point(ClientSize.Width * 3 / 4 - 40, ClientSize.Height * 2 / 5);
            var size = new Size(200, 75);
            foreach (var elem in heads)
            {
                var button = CreateButton(elem, initialPosition, size);
                if (elem == "Continue.png")
                {
                    button.PressButton();
                    button.Enabled = false;
                }
                button.SetButton(this);
                buttons.Add(button);
                initialPosition.Y += size.Height + 30;
            }
            return buttons;
        }

        public Menu()
        {
            InitStartMenu();
            
            var musicMuter = CreateButton("audio.png", new Point(Right - 70, Top + 10), new Size(50, 50));
            musicMuter.SetAudioButton(audioManager);

            var soundMuter = CreateButton("sounds.png", new Point(musicMuter.Location.X - 50 - 20, Top + 10), new Size(50, 50));
            soundMuter.SetAudioButton(audioManager);
            
            var buttons = InitMainButtons();

            Paint += (sender, args) =>
            {
                var g = args.Graphics;
                g.DrawImage(Image.FromFile(pathToSprites + "menu.png"), 25, 25, 500, 400);
                g.DrawImage(Image.FromFile(pathToSprites + "icon.png"), 600, 100, 100, 100);
                g.DrawImage(musicMuter.BackgroundImage, musicMuter.Location.X, musicMuter.Location.Y, musicMuter.Size.Width, musicMuter.Size.Height);
                g.DrawImage(soundMuter.BackgroundImage, soundMuter.Location.X, soundMuter.Location.Y, soundMuter.Size.Width, soundMuter.Size.Height);
                foreach (var elem in buttons)
                {
                    g.DrawImage(elem.BackgroundImage, elem.Location.X, elem.Location.Y, elem.Size.Width, elem.Size.Height);
                }
            };

            MouseDown += (sender, args) =>
            {
                var cursor = args.Location;
                audioManager.PlaySound();
                if (cursor.InBounds(new Rectangle(musicMuter.Location, musicMuter.Size)))
                {
                    musicMuter.PerformClick();
                    Refresh();
                }
                else if (cursor.InBounds(new Rectangle(soundMuter.Location, soundMuter.Size)))
                {
                    soundMuter.PerformClick();
                    Refresh();
                }
                else
                {
                    foreach (var button in buttons)
                    {
                        if (cursor.InBounds(new Rectangle(button.Location, button.Size)) && button.Enabled)
                        {
                            button.PressButton();
                            Refresh();
                            break;
                        }
                    }
                }
                
            };
            MouseUp += (sender, args) =>
            {
                var cursor = args.Location;
                foreach (var button in buttons)
                {
                    if (cursor.InBounds(new Rectangle(button.Location, button.Size)))
                    {
                        if (button.Enabled)
                        {
                            button.UnPressButton();
                            Refresh();
                            button.PerformClick();
                            break;
                        }
                    }
                    else
                    {
                        if (button.Enabled)
                        {
                            button.UnPressButton();
                            Refresh();
                        }
                    }
                }
            };
        }
    }
}
