using Services.Sound;
using UnityEngine;
using Zenject;

namespace Logic.Sounds
{
    public class BackgroundMusic : MonoBehaviour
    {
        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;

        private void Start() =>
            _soundService.BackgroundMusicActivation(_soundService.MusicActivity);
    }
}