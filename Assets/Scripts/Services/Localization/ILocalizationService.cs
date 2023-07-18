using System;

namespace Services.Localization
{
    public interface ILocalizationService
    {
        public Languages CurrentLanguage { get; set; }
        public event Action LanguageChanged;

        public void LoadTranslations();
        public void SetCurrentLanguage(Languages language);
        public void SwitchLanguage();
        public string GetTextTranslationByKey(string key);
    }
}