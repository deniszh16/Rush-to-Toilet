using System;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class Translation
    {
        [Header("Ключ перевода")]
        public string Key;

        [Header("Переводы")]
        public string Russian;
        public string English;
    }
}