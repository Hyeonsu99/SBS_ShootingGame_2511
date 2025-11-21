using UnityEngine;

public class PlayerInput : MonoBehaviour, IInputHandler
{
    private IInputHandler curInput;
    private void Awake()
    {
#if UNITY_STANDALONE || UNITY_EDITOR    // PC빌드 & 에디터
        curInput = GetComponent<Input_Keyboard>() as IInputHandler;
#endif

#if UNITY_ANDROID || UNITY_IOS          // 안드로이드
        curInput = GetComponent<Input_Joystick>() as IInputHandler;
#endif
    }
    public Vector2 GetInput()
    {
        if (curInput == null)
        {
            Debug.Log("PlayerInput.cs - GetInput() - curInput is Null");
            return Vector2.zero;
        }
        return curInput.GetInput();
    }

    void Update()
    {
        //Debug.Log($"Input Check : {GetInput()}");
    }
}
