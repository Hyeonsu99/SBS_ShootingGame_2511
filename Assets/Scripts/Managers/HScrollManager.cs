using System.Collections.Generic;
using UnityEngine;

public class HScrollManager : MonoBehaviour, IManager
{
    private List<IScroller> scrollers = new List<IScroller>();
    [SerializeField] private float scrollSpeed = 4;
    public void GameInitialize()
    {
        scrollers.Clear();
        scrollers = InterfaceFinder.FindObjectsOfInterface<IScroller>();
    }

    public void GameStart()
    {
        foreach (IScroller c in scrollers)
        {
            c.SetScrollSpeed(scrollSpeed);
        }
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
        foreach(IScroller c in scrollers)
        {
            c.Scroll(delta);
        }
    }

}
