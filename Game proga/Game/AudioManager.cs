using System.Windows.Forms;
using System.Media;
using WMPLib;

namespace Game
{
    public class AudioManager
    {
        bool isSoundOn = true;
        bool isMusicOn = true;
        public SoundPlayer SoundMixer { get; }
        public WindowsMediaPlayer MusicMixer { get; }

        public AudioManager(SoundPlayer muter, WindowsMediaPlayer mixer)
        {
            SoundMixer = muter;
            MusicMixer = mixer;
        }

        public void ChangeSoundStatus() => isSoundOn = !isSoundOn;

        public void UnMuteMusic() { if (!isMusicOn) MusicMixer.controls.play(); isMusicOn = true; }

        public void MuteMusic() { if (isMusicOn) MusicMixer.controls.pause(); isMusicOn = false; }

        public void PlaySound() { if (isSoundOn) SoundMixer.Play(); }

        public bool GetIsSoundOn => isSoundOn;
        public bool GetIsMusicOn => isMusicOn;
    }
}
