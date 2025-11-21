using UnityEngine;

// 템플릿 -> 제네릭
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
        DoAwake();
    }

    protected virtual void DoAwake() { }
}

public class SingletinDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
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
        {
            Destroy(gameObject);
        }
        DoAwake();
    }

    protected virtual void DoAwake() { }
}
