using Services.Localization;
using UnityEngine;
using Zenject;
using TMPro;

namespace Logic.UI
{
    public class TextTranslation : MonoBehaviour
    {
        [Header("Ключ перевода")]
        [SerializeField] private string _key;
        
        [Header("Текстовый компонент")]
        [SerializeField] private TextMeshProUGUI _textComponent;
        
        private ILocalizationService _localizationService;

        [Inject]
        private void Construct(ILocalizationService localizationService) => 
            _localizationService = localizationService;
        
        private void Awake() => 
            _localizationService.LanguageChanged += TranslateText;

        private void Start() =>
            TranslateText();

        private void TranslateText() =>
            _textComponent.text = _localizationService.GetTextTranslationByKey(_key);
        
        public void ChangeKey(string value)
        {
            _key = value;
            TranslateText();
        }
        
        private void OnDestroy() => 
            _localizationService.LanguageChanged -= TranslateText;
    }
}