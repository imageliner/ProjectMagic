using System;
using System.Collections;
using UnityEngine;

public class HitstopManager : MonoBehaviour
{
    private bool isHitStopping = false;

    public Action HitStop;

    public void DoHitStop(float duration)
    {
        if (isHitStopping)
        {
            return;
        }

        StartCoroutine(HitStopCoroutine(duration));
    }

    private IEnumerator HitStopCoroutine(float duration)
    {
        isHitStopping = true;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        isHitStopping = false;
    }
}
