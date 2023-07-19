using Services.Sound;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class MusicSwitchButton : MonoBehaviour
    {
        [Header("Кнопка переключения")]
        [SerializeField] private Button _button;

        [Header("Иконки состояния")]
        [SerializeField] private GameObject _active;
        [SerializeField] private GameObject _inactive;

        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;
        
        private void Awake()
        {
            _button.onClick.AddListener(ChangeMusicSetting);
            _soundService.MusicChanged += ChangeButtonIcon;
        }

        private void Start() =>
            ChangeButtonIcon();

        private void ChangeMusicSetting() =>
            _soundService.SwitchMusic();

        private void ChangeButtonIcon()
        {
            bool musicActivity = _soundService.MusicActivity;
            _active.SetActive(musicActivity);
            _inactive.SetActive(!musicActivity);
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ChangeMusicSetting);
            _soundService.MusicChanged -= ChangeButtonIcon;
        }
    }
}