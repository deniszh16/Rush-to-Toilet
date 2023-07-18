using System;
using System.Collections.Generic;
using System.Linq;
using Services.PersistentProgress;
using Services.SaveLoad;
using StaticData;
using UnityEngine;

namespace Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public Languages CurrentLanguage { get; set; }
        public event Action LanguageChanged;

        private Dictionary<string, Translation> TranslationsDictionary { get; set; }
        private const string PathTranslations = "StaticData/TranslationsStaticData";
        
        public void LoadTranslations()
        {
            TranslationsDictionary = Resources.Load<TranslationsStaticData>(PathTranslations)
                .ListOfTranslations.ToDictionary(x => x.Key, x => x);
        }

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public LocalizationService(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void SetCurrentLanguage(Languages language)
        {
            CurrentLanguage = language;
            LanguageChanged?.Invoke();
        }

        public void SwitchLanguage()
        {
            Languages language = _progressService.UserProgress.LanguageData.Language;
            if (language == Languages.Russian)
            {
                _progressService.UserProgress.LanguageData.SetLanguage(Languages.English);
                CurrentLanguage = Languages.English;
            }
            else
            {
                _progressService.UserProgress.LanguageData.SetLanguage(Languages.Russian);
                CurrentLanguage = Languages.Russian;
            }
            
            LanguageChanged?.Invoke();
            _saveLoadService.SaveProgress();
        }

        public string GetTextTranslationByKey(string key)
        {
            if (CurrentLanguage == Languages.Russian)
                return TranslationsDictionary[key].Russian;
            
            return TranslationsDictionary[key].English;
        }
    }
}