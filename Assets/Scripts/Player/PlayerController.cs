using UnityEngine;

public class PlayerController : MonoBehaviour, IManager
{
    private IMovement movement;
    private IInputHandler input;
    private IWeapon curWeapon;

    public void GameInitialize()
    {
        movement = GetComponent<PlayerMovement>() as IMovement;
        input = GetComponent<PlayerInput>() as IInputHandler;
        curWeapon = GetComponent<PlayerWeapon>() as IWeapon;
    }
    public void GameStart()
    {
        movement?.SetEnable(true);
        curWeapon?.SetEnbale(true);
    }
    public void GamePause()
    {

    }
    public void GameResume()
    {

    }
    public void GameOver()
    {

    }
    public void GameTick(float delta)
    {
        if (movement == null)
            return;
        if (input == null)
            return;

        movement.Move(input.GetInput(),delta);

        curWeapon?.SetFire();

    }
}
