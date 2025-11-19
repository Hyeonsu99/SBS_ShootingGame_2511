using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    private bool isMoving = false;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 minArea = new Vector2(-2f, -4.5f);
    private Vector2 maxArea = new Vector2(2f, 0f);

    private Vector3 moveDelta;

    public void Move(float delta, Vector2 direction)
    {
        if (isMoving)
        {
            moveDelta = new Vector3(direction.x, direction.y, 0f) * (moveSpeed * delta);   // 배속 설정을 위해서는 따로 시간 배율값을 계산에 적용해야 함

            Vector3 newPosition = transform.position + moveDelta;

            newPosition.x = Mathf.Clamp(newPosition.x, minArea.x, maxArea.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minArea.y, maxArea.y);

            transform.position = newPosition;
        }
    }

    public void SetEnable(bool newEnable)
    {
        isMoving = newEnable;
    }
}
