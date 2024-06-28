using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    static Camera mainCam;
    public static Camera MainCam
    {
        get 
        {
            if (mainCam == null) mainCam = Camera.main;
            return mainCam;
        }

    }

    public static T ChangeAlpha<T>(this T g, float newAlpha)
        where T : Graphic
    {
        var color = g.color;
        color.a = newAlpha;
        g.color = color;
        return g;
    }

    public static float Lerp(float a, float b, float t) => a + (b - a) * t;

    public static T GetRandomItem<T>(this IEnumerable<T> arr) 
    {
        int i = 0;
        int randomNum = Random.Range(0, arr.Count());
        foreach (var item in arr)
        {
            if (i++ == randomNum) return item;
        }
        return default;
    }
}
