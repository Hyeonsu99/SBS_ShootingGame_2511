using UnityEngine;

public class PlayerInput : MonoBehaviour, IInputHandler
{
    private IInputHandler curInputHandle;

    private void Awake()
    {
#if UNITY_STANDALONE || UNITY_EDITOR    // PC 빌드 & 에디터
        curInputHandle = GetComponent<Input_Keyboard>() as IInputHandler;
#endif
#if UNITY_ANDROID || UNITY_IOS          // 모바일
        curInputHandle = GetComponent<Input_Joystick>() as IInputHandler;
#endif
    }
    public Vector2 GetInput()
    {
        if(curInputHandle == null)
        {
            Debug.Log("PlayerInput.cs() - GetInput() - curInputHandler is NULL");
            return Vector2.zero;
        }

        return curInputHandle.GetInput();
    }

    private void Update()
    {

    }
}
