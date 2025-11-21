using UnityEngine;

// Á¦³×¸¯ ½Ì±ÛÅæ
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
            Destroy(gameObject);
        OnAwake();
    }

    protected virtual void OnAwake() { }
}

public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        OnAwake();
    }

    protected virtual void OnAwake() { }
}