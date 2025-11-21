using UnityEngine;

public class PlayerInput : MonoBehaviour, IInputHandler
{
    private IInputHandler curInputHandler;

    private void Awake()
    {
#if UNITY_STANDALONE || UNITY_EDITOR // PC 빌드 & 에디터
        curInputHandler = GetComponent<InputKeyboard>() as IInputHandler;
#endif

#if UNITY_ANDROID || UNITY_IOS // 
curInputHandler = GetComponent<InputJoystick>() as IInputHandler;
#endif
    }

    public Vector2 GetInput()
    {
        if (curInputHandler == null)
        {
            Debug.Log("PlayerInput.cs - GetInput() - curInputHandle is null");
            return Vector2.zero;
        }
        return curInputHandler.GetInput();
    }

    private void Update()
    {
        //Debug.Log($"입력값 확인 : {GetInput()}");
    }
}
