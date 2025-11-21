using UnityEngine;

public class PlayerController : MonoBehaviour, IManager
{
    private IMovement movement;
    private IInputHandler inputHandler;
    private IWeapon curWeapon;

    public void GameInitialize()
    {
        inputHandler = GetComponent<PlayerInput>() as IInputHandler;
        movement = GetComponent<PlayerMovement>() as IMovement;
        curWeapon = GetComponent<PlayerWeapon>() as IWeapon;

    }

    public void GameOver()
    {
        
    }

    public void GamePause()
    {
       
    }

    public void GameResume()
    {
       
    }

    public void GameStart()
    {
        movement?.SetEnable(true);  // 게임 시작할 때 활성화 해야함.
        curWeapon?.SetEnable(true);
    }

    public void GameTick(float delta)
    {
        if (movement == null) return;
        if (inputHandler == null) return;

        movement?.Move(delta, inputHandler.GetInput());
        curWeapon?.SetFire();
    }
}
