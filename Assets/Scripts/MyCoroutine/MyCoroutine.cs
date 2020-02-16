using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyCoroutine
{
    public static IEnumerator WaitInRealSeconds(float time)
    {
        float start = Time.unscaledTime;
        while (Time.unscaledTime < (start + time))
        {
            yield return null;
        }
    }  
}
