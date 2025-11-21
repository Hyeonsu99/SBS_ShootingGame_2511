using UnityEngine;

public class Input_Joystick : MonoBehaviour, IInputHandler
{
    private FixedJoystick joystick;
    private void Awake()
    {
        joystick = FindAnyObjectByType<FixedJoystick>();
        if (joystick == null)
        {
            Debug.Log("Input_Joystick.cs - Awake() - Cannot Find Joystick.");
        }
    }
    public Vector2 GetInput()
    {
        if (joystick == null)
            return Vector2.zero;
        return new Vector2(joystick.Horizontal, joystick.Vertical);
    }
}
