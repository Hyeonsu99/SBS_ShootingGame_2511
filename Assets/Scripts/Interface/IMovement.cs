using UnityEngine;

public interface IMovement
{
    void Move(float delta, Vector2 direction);
    void SetEnable(bool newEnable);
}
