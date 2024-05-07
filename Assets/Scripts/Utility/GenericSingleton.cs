using UnityEngine;

public abstract class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _instance;
    public static T Instance 
    {
        get 
        {
            _instance ??= FindInScene();
            DontDestroyOnLoad(_instance);
            return _instance ??= GenerateSingleton();
        }
    }
    private static T FindInScene() => FindObjectOfType<T>();
    private static T GenerateSingleton()
    {
        GameObject singletonObject = new GameObject(typeof(T).Name + " Singleton");
        DontDestroyOnLoad(singletonObject);
        return singletonObject.AddComponent<T>();
    }
}