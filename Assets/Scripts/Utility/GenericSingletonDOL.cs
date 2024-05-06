using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Destroy on load variant of singleton 
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class GenericSingletonDOL<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        
        public static T Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}