using UnityEngine;

public interface IScroller
{
    void Scroll(float deltaTime);
    void ResetPosition();
    void SetScrollSpeed(float newSpeed);
}
