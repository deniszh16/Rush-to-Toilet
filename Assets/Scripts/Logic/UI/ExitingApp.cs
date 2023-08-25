using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class ExitingApp : MonoBehaviour
    {
        [Header("Кнопка выхода")]
        [SerializeField] private Button _button;

        private void Awake() =>
            _button.onClick.AddListener(QuitGame);

        private void QuitGame() =>
            Application.Quit();

        private void OnDestroy() =>
            _button.onClick.RemoveListener(QuitGame);
    }
}