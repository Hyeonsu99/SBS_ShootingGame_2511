using System.Collections.Generic;
using UnityEngine;
using System.Linq; // C#에서 Quarry 문법을 제공하는 헤더

public class InterfaceFinder : MonoBehaviour
{
    // Linq 쓰지 않는 함수
    //public static List<T> FindObjectsOfInterface<T>() where T : class
    //{
    //    MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
    //    List<T> list = new List<T>();
    //    foreach (var obj in allObjects)
    //    {
    //        if (obj is T interfaceInstance)
    //        {
    //            list.Add(interfaceInstance);
    //        }
    //    }
    //    return list;
    //}

    //Linq 쓰는 함수
    public static List<T> FindObjectsOfInterface<T>() where T : class
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<T>().ToList();
    }
}
