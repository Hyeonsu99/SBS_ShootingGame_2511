using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    private bool isMove = false;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 minArea = new Vector2(-2f, -4.5f);
    private Vector2 maxArea = new Vector2(2f, 0);

    private Vector3 moveDelta;

    public void Move(float delta,Vector2 direction)
    {
        if(isMove)
        {
            moveDelta = new Vector3(direction.x, direction.y, 0f) * (moveSpeed * delta);

            Vector3 newPosition = transform.position + moveDelta;

            newPosition.x = Mathf.Clamp(newPosition.x, minArea.x, maxArea.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minArea.y, maxArea.y);

            transform.position = newPosition;
        }
    }

    public void SetEnable(bool newEnable)
    {
        isMove = newEnable;
    }
}
