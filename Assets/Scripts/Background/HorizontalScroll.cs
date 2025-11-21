using UnityEngine;

public class HorizontalScroll : MonoBehaviour, IScroller
{
    [SerializeField] private float scrollSpeed = 0;
    private Vector3 startPos = new Vector3(0f, 12.75f, 0f);
    private float resetPositionY = -12.75f;

    public void ResetPosition()
    {
        transform.position = startPos;
    }

    public void Scroll(float deltaTime)
    {
        transform.position += Vector3.down * (scrollSpeed * deltaTime);

        if(transform.position.y < resetPositionY)
            ResetPosition();
    }

    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}
