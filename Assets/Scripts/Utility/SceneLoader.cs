using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class SceneLoader : GenericSingleton<SceneLoader>
    {
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
        }

        public void LoadSceneAsync(string sceneName, bool additive = false)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName, additive));
        }
    }
}