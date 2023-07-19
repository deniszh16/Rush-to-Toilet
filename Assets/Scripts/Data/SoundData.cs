using System;

namespace Data
{
    [Serializable]
    public class SoundData
    {
        public bool SoundActivity;
        public bool MusicActivity;

        public SoundData()
        {
            SoundActivity = true;
            MusicActivity = true;
        }

        public void ChangeActivityOfSounds(bool value) =>
            SoundActivity = value;
        
        public void ChangeActivityOfMusic(bool value) =>
            MusicActivity = value;
    }
}