using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private bool ticking = true;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float timeScale = 1.0f;

    [SerializeField]
    [Range(1, 100)]
    private float tickLength = 60.0f;

    private float remainingTicks = 0;


    public delegate void ResetLoopFunc();
    private ResetLoopFunc ResetLoop;

    public void SetCallback(ResetLoopFunc callback)
    {
        ResetLoop += callback;
    }

    void StartTicking()
    {
        remainingTicks = tickLength;
        ticking = true;
    }

    void PauseTicking()
    {
        ticking = false;
    }

    void ResumeTicking()
    {
        ticking = true;
    }

    void FixedUpdate()
    {
        if (!ticking)
        {
            return;
        }

        remainingTicks -= Time.fixedDeltaTime * timeScale;
        if (remainingTicks <= 0)
        {
            ResetLoop?.Invoke();
            PauseTicking();
        }
    }
}
