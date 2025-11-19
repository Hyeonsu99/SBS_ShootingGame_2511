using UnityEngine;

public class PlayerController : MonoBehaviour, IManager
{
    private IMovement movement;
    private IInputHandler inputHandler;

    public void GameInitialize()
    {
        inputHandler = GetComponent<PlayerInput>() as IInputHandler;
        movement = GetComponent<PlayerMovement>() as IMovement;

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
        movement?.SetEnable(true);  // 게임 시작할 때 활성화 해야함.
    }

    public void GameTick(float delta)
    {
        if (movement == null) return;
        if (inputHandler == null) return;

        movement.Move(delta, inputHandler.GetInput());
    }
}
