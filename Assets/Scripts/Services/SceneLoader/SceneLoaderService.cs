using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class SceneLoaderService : MonoBehaviour, ISceneLoaderService
    {
        [Header("Экран затемнения")]
        [SerializeField] private CanvasGroup _blackout;
        
        private const float DimmingSpeed = 0.02f;
        private const float DimmingStep = 0.1f;
        
        public void LoadSceneAsync(Scenes scene, bool screensaver, float delay) =>
            StartCoroutine(LoadSceneAsyncCoroutine(scene, screensaver, delay));
        
        private IEnumerator LoadSceneAsyncCoroutine(Scenes scene, bool screensaver, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (screensaver)
            {
                _blackout.blocksRaycasts = true;
                while (_blackout.alpha < 1)
                {
                    yield return new WaitForSeconds(DimmingSpeed);
                    _blackout.alpha += DimmingStep;
                }
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
            while (asyncOperation.isDone != true)
                yield return null;

            if (screensaver)
            {
                _blackout.blocksRaycasts = false;
                while (_blackout.alpha > 0)
                {
                    yield return new WaitForSeconds(DimmingSpeed);
                    _blackout.alpha -= DimmingStep;
                }
            }
        }
    }
}