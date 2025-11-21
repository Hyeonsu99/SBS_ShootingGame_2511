using UnityEngine;

public interface IMovement 
{
    void Move(Vector2 direction, float delta);           // 오브젝트 이동
    void SetEnable(bool newEnable);         // 활성화 제어
}
