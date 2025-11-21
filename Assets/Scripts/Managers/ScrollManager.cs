using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ScrollManager : MonoBehaviour, IManager
{
    private List<IScroller> scrollers = new List<IScroller>();

    [SerializeField] private float scrollSpeed = 4f;

    public void GameInitialize()
    {
        scrollers.Clear();
        scrollers = InterfaceFinder.FindObjectOfInterface2<IScroller>();

       //FindObjectsByType<IScroller>(FindObjectsSortMode.None);
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
       foreach(IScroller c in scrollers)
        {
            c.SetScrollSpeed(scrollSpeed);
        }
    }

    public void GameTick(float delta)
    {
        foreach(IScroller c in scrollers)
        {
            c.Scroll(delta);
        }
    }

}
