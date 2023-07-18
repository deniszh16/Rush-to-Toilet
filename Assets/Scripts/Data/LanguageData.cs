using System;
using Services.Localization;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LanguageData
    {
        public Languages Language;

        public LanguageData() =>
            Language = Application.systemLanguage == SystemLanguage.Russian ? Languages.Russian : Languages.English;

        public void SetLanguage(Languages language) =>
            Language = language;
    }
}