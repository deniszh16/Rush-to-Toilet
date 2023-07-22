using Services.Sound;
using UnityEngine;
using Zenject;

namespace Logic.Sounds
{
    public class PlayingSound : MonoBehaviour
    {
        [Header("Компонент звука")]
        [SerializeField] private AudioSource _audioSource;

        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;

        public void PlaySound()
        {
            if (_soundService.SoundActivity && _audioSource.isPlaying == false)
                _audioSource.Play();
        }

        public void StopSound() =>
            _audioSource.Stop();
    }
}