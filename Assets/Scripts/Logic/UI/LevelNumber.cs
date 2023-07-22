using Logic.Levels;
using UnityEngine;
using TMPro;

namespace Logic.UI
{
    public class LevelNumber : MonoBehaviour
    {
        [Header("Необходимые компоненты")]
        [SerializeField] private LevelTasks _levelTasks;

        [Header("Компонент перевода")]
        [SerializeField] private TextTranslation _textTranslation;
        [SerializeField] private TextMeshProUGUI _textComponent;

        private void Start()
        {
            _textTranslation.TranslateText();
            
            if (_levelTasks.Number < 10)
                _textComponent.text += " 0" + _levelTasks.Number;
            else
                _textComponent.text += " " + _levelTasks.Number;
        }
    }
}