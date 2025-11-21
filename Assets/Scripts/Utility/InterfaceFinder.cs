using System.Collections.Generic;
using UnityEngine;
using System.Linq; // C# 쿼리 문법 제공

public class InterfaceFinder : MonoBehaviour
{
    public static List<T> FindObjectsOfInterface<T>() where T : class
    {
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        List<T> list = new List<T>();

        foreach (var obj in allObjects)
        {
            if(obj is T interfaceInst)
                list.Add(interfaceInst);
        }
        return list;
    }

    public static List<T> FindObjectsOfInterface2<T>() where T : class
    {
        // 찾은 것들을 T타입으로 형변환 가능한지 체크
        // 형변환 가능하면 리스트에 추가
        return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<T>().ToList();
    }
}
