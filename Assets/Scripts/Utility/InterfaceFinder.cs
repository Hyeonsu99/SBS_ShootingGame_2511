using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // C#에서 제공하는 쿼리 문법

public class InterfaceFinder : MonoBehaviour
{
    public static List<T> FindObjectsOfInterface<T>() where T : class
    {
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        List<T> list = new List<T>();

        foreach(var obj in allObjects)
        {
            if(obj is T interfaceInstance)
                list.Add(interfaceInstance);
        }

        return list;
    }

    public static List<T> FindObjectOfInterface2<T>() where T : class
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<T>()
            .ToList();
    }

}
