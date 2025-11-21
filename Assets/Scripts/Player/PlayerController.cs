using UnityEngine;

public class PlayerController : MonoBehaviour, IManager
{
    private IMovement movement;
    private IInputHandler inputHandler;
    private IWeapon curWeapon;

    public void GameInit()
    {
        inputHandler = GetComponent<PlayerInput>() as IInputHandler;// 형변환
        movement = GetComponent<IMovement>();
        curWeapon = GetComponent<IWeapon>();
    }

    public void GameOver()
    {
        throw new System.NotImplementedException();
    }

    public void GamePause()
    {
        throw new System.NotImplementedException();
    }

    public void GameResume()
    {
        throw new System.NotImplementedException();
    }

    public void GameStart()
    {
        movement?.SetEnable(true); // 게임 시작 할때 활성화해야함.
        curWeapon?.SetEnable(true);
    }

    public void GameTick(float delta)
    {
        if (movement == null)
            return;
        if (inputHandler == null)
            return;

        movement.Move(delta, inputHandler.GetInput());
        curWeapon?.SetFire();

    }
}
