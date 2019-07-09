using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _instance;

    private static object _lock = new object();

    public static T Instance {
        get {
            if (applicationIsQuitting) {
                return null;
            }

            lock (_lock) {
                if (_instance == null) {
                    _instance = (T)FindObjectOfType(typeof(T));
                    var ts = FindObjectsOfType(typeof(T));

                    if (ts.Length > 1) {
                        for (int i = 1; i < ts.Length; i++)
                            Destroy(ts[i]);
                    }

                    if (_instance == null) {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                    }
                    _instance.name= "(Singleton) " + typeof(T).ToString();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    public void OnDestroy() {
        applicationIsQuitting = true;
    }
}