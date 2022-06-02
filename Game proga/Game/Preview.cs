using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace Game
{
    class Preview : ILevel
    {
        public Menu MainForm { get; }
        bool clicked;

        public Preview(Menu form)
        {
            MainForm = form;
        }

        public void Initialize()
        {
            var manager = Menu.audioManager;
            MainForm.ClearInvocationList("Paint");
            MainForm.ClearInvocationList("MouseDown");
            MainForm.ClearInvocationList("MouseUp");
            MainForm.BackgroundImage = null;
            MainForm.BackColor = Color.Black;
            MainForm.Click += (sender, args) => { clicked = true; };
            if (manager.GetIsSoundOn) MainForm.Click += (sender, args) => manager.PlaySound();
            MainForm.Refresh();
            manager.MusicMixer.controls.stop();

            MakePause(1500);

            if (manager.GetIsMusicOn) SetUpAudioManager(manager);
            InitPreStory();
        }

        private void InitPreStory()
        {
            PrintText("Нет ничего лучше, чем мир во всём мире.", new Point(50, 100), Brushes.White, new Font("Times New Roman", 25));
            MakePause(1000);
            PrintText("Был обычный спокойный вечер.", new Point(50, 170), Brushes.White, new Font("Times New Roman", 25));
            MakePause(1000);
            PrintText("Ничего не предвещало беды, как вдруг...", new Point(50, 240), Brushes.White, new Font("Times New Roman", 25));
            MakePause(1000);
            PrintText("кликните, чтобы продолжить...", new Point(500, 550), Brushes.White, new Font("Times New Roman", 14));
            while (!clicked) MakePause(100);
            InitRulesWindow();
            while (!clicked) MakePause(100);
        }

        private void SetUpAudioManager(AudioManager manager)
        {
            manager.MusicMixer.URL = Menu.pathToAudio + "военная музыка2.mp3";
            manager.MusicMixer.settings.volume = 10;
            manager.MusicMixer.settings.autoStart = false;
        }

        public void MakePause(int timeInMilliseconds) => Thread.Sleep(timeInMilliseconds);

        public void PrintText(string text, Point location, Brush brush, Font font)
        {
            if (clicked) clicked = false;
            var time = 30;
            var result = "";
            MainForm.Paint += (sender, args) => args.Graphics.DrawString(result, font, brush, location);
            foreach (var elem in text)
            {
                if (clicked)
                {
                    result = text;
                    MainForm.Refresh();
                    break;
                }
                result += elem;
                MakePause(time);
                MainForm.Refresh();
            }
            clicked = false;
        }
        public void InitRulesWindow()
        {
            MainForm.ClearInvocationList("Paint");
            MainForm.BackgroundImage = Image.FromFile(Menu.pathToSprites + "фон21.jpg");
            MainForm.Paint += (sender, args) => args.Graphics.DrawImage(Image.FromFile(Menu.pathToSprites + "фон опций.png"), MainForm.Width / 2 - 250, MainForm.Height / 2 - 200, 500, 420);
            MainForm.Refresh();
            MakePause(1000);
            PrintText("Задача:", new Point(205, 170), Brushes.Black, new Font("Times New Roman", 20, FontStyle.Bold));
            MakePause(500);
            PrintText("Защитить дома любой ценой", new Point(205, 230), Brushes.Black, new Font("Times New Roman", 20, FontStyle.Bold));
            MakePause(500);
            PrintText("Нажимайте на бомбы левой кнопкой", new Point(205, 300), Brushes.Black, new Font("Times New Roman", 16, FontStyle.Bold));
            PrintText("мыши, чтобы взорвать. В верхнем ", new Point(205, 330), Brushes.Black, new Font("Times New Roman", 16, FontStyle.Bold));
            PrintText("правом углу находится время,", new Point(205, 360), Brushes.Black, new Font("Times New Roman", 16, FontStyle.Bold));
            PrintText("которое осталось продержаться.", new Point(205, 390), Brushes.Black, new Font("Times New Roman", 16, FontStyle.Bold));
            PrintText("Удачи!", new Point(205, 440), Brushes.Black, new Font("Times New Roman", 20, FontStyle.Bold));
        }
    }
}
