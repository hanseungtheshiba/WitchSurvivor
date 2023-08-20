using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shutdown = false;
    private static object shutdownLock = new object();
    private static T instance = null;
    public static T Instance {
        get
        {
            if (shutdown)
            {
                instance = null;
                return null;
            }                

            lock(shutdownLock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        GameObject temp = new GameObject(typeof(T).ToString());
                        instance = temp.AddComponent<T>();
                    }
                }
            }

            return instance;
        }
    }

    private void OnDestroy()
    {
        shutdown = true;
    }

    private void OnApplicationQuit()
    {
        shutdown = true;
    }
}
