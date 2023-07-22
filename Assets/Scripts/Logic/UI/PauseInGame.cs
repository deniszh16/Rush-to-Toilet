using UnityEngine;

namespace Logic.UI
{
    public class PauseInGame : MonoBehaviour
    {
        public void PauseSetting(bool state) =>
            Time.timeScale = state ? 0f : 1f;

        private void OnDestroy() =>
            PauseSetting(state: false);
    }
}