using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "TranslationsStaticData", menuName = "StaticData/Translations")]
    public class TranslationsStaticData : ScriptableObject
    {
        [Header("Список переводов")]
        public List<Translation> ListOfTranslations;
    }
}