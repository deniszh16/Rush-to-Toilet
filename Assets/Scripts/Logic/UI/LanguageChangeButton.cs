using Services.Localization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class LanguageChangeButton : MonoBehaviour
    {
        [Header("Спрайты кнопок")]
        [SerializeField] private Sprite[] _sprites;

        [Header("Кнопка переключения")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _imageButton;
        
        private ILocalizationService _localizationService;

        [Inject]
        private void Construct(ILocalizationService localizationService) =>
            _localizationService = localizationService;
        
        private void Awake()
        {
            _button.onClick.AddListener(ChangeLanguage);
            _localizationService.LanguageChanged += SetButtonSprite;
        }
        
        private void Start() =>
            SetButtonSprite();
        
        private void ChangeLanguage() =>
            _localizationService.SwitchLanguage();
        
        private void SetButtonSprite()
        {
            int spriteNumber = _localizationService.CurrentLanguage == Languages.Russian ? 0 : 1;
            _imageButton.sprite = _sprites[spriteNumber];
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ChangeLanguage);
            _localizationService.LanguageChanged -= SetButtonSprite;
        }
    }
}