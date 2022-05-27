using System.Windows.Forms;
using System.Drawing;
using System.Media;
using WMPLib;
namespace Game
{
    static class ButtonExtensions
    {
        public static readonly string pathToSprites = @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\";
        public static void PressButton(this Button button)
        {
            var head = (button.Name).Remove(button.Name.Length - 4, 4) + "Pressed.png";
            var path = pathToSprites + head;
            button.BackgroundImage = Image.FromFile(path);
            button.Name = head;
        }
        public static void UnPressButton(this Button button)
        {
            var head = button.Name.Replace("Pressed", "");
            var path = pathToSprites + head;
            button.BackgroundImage = Image.FromFile(path);
            button.Name = head;
        }

        public static void SetButton(this Button button, Menu form)
        {
            switch (button.Name)
            {
                case "Play.png":
                    button.Click += (sender, args) =>
                    {
                        var first = new FirstLevel(form);
                        first.Initialize();
                    };
                    break;
                case "Continue.png":
                    break;
                case "Exit.png":
                    button.Click += (sender, args) =>
                    {
                        Application.Exit();
                    };
                    break;
            }
        }

        public static void SetAudioButton(this Button button, AudioManager manager)
        {
            button.Click += (sender, args) =>
            {
                if (button.Name.Contains("audio"))
                {
                    if (!button.Name.Contains("Pressed"))
                    {
                        PressButton(button);
                        manager.MuteMusic();
                    }
                    else
                    {
                        UnPressButton(button);
                        manager.UnMuteMusic();
                    }
                }
                else
                {
                    if (!button.Name.Contains("Pressed"))
                    {
                        PressButton(button);
                        manager.ChangeSoundStatus();
                    }
                    else
                    {
                        UnPressButton(button);
                        manager.ChangeSoundStatus();
                    }
                }
            }; 
        }
    }
}
