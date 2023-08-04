using System;

namespace Services.Sound
{
    public interface ISoundService
    {
        public bool SoundActivity { get; set; }
        public bool MusicActivity { get; set; }
        
        public event Action SoundChanged;
        public event Action MusicChanged;
        
        public void SwitchSound();
        public void SwitchMusic();
        public void BackgroundMusicActivation(bool state);
    }
}