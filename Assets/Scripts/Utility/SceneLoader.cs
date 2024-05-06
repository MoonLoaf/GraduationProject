using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class SceneLoader : GenericSingleton<SceneLoader>
    {
        public delegate void SceneLoadedEvent();
        public static event SceneLoadedEvent OnSceneLoaded;
        private IEnumerator LoadSceneAsyncCoroutine(string sceneName, bool additive = false)
        {
            AsyncOperation asyncOperation;

            if (additive)
            {
                asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            }

            while (!asyncOperation.isDone)
            {
                // float progress = asyncOperation.progress;
                yield return null;
            }
            OnSceneLoaded?.Invoke();
        }

        public void LoadSceneAsync(string sceneName, bool additive = false)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName, additive));
        }
    }
}