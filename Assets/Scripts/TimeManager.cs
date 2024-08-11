using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    private bool ticking = false;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float timeScale = 1.0f;

    [SerializeField]
    [Range(1, 100)]
    private float tickLength = 60.0f;

    private float remainingTicks;


    public delegate void ResetLoopFunc();
    private ResetLoopFunc ResetLoop;

    void Start()
    {
        remainingTicks = tickLength;
    }

    public void SetCallback(ResetLoopFunc callback)
    {
        ResetLoop += callback;
    }

    public void StartTicking()
    {
        remainingTicks = tickLength;
        ticking = true;
    }

    public void PauseTicking()
    {
        ticking = false;
    }

    public void ResumeTicking()
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
