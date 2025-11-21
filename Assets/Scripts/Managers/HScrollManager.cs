using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HScrollManager : MonoBehaviour, IManager
{
    private List<IScroller> scrollers = new List<IScroller>();

    [SerializeField] private float scrollSpeed = 4f;

    public void GameInit()
    {
        scrollers.Clear();
        scrollers = InterfaceFinder.FindObjectsOfInterface2<IScroller>();
        //FindObjectsByType<IScroller>(FindObjectsSortMode.None);
        // interfacefind
    }

    public void GameOver()
    {

    }

    public void GamePause()
    {

    }

    public void GameResume()
    {

    }

    public void GameStart()
    {
        foreach(var c  in scrollers)
        {
            c.SetScrollSpeed(scrollSpeed);
        }
    }

    public void GameTick(float delta)
    {
        foreach(var c in scrollers)
        {
            c.Scroll(delta);
        }
    }
}
