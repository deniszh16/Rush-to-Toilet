using Services.Sound;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class SoundSwitchButton : MonoBehaviour
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
            _button.onClick.AddListener(ChangeSoundSetting);
            _soundService.SoundChanged += ChangeButtonIcon;
        }

        private void Start() =>
            ChangeButtonIcon();

        private void ChangeSoundSetting() =>
            _soundService.SwitchSound();

        private void ChangeButtonIcon()
        {
            bool soundActivity = _soundService.SoundActivity;
            _active.SetActive(soundActivity);
            _inactive.SetActive(!soundActivity);
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ChangeSoundSetting);
            _soundService.SoundChanged -= ChangeButtonIcon;
        }
    }
}