using UnityEngine;

// C++ ÅÆÇÃ¸´ --> C# Á¦³×¸¯
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }

        DoAwake();
    }

    protected virtual void DoAwake()
    {

    }
}



public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }

        DoAwake();
    }

    protected virtual void DoAwake()
    {

    }
}
