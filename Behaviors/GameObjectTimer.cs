using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTimer : MonoBehaviour
{

    private float timeLeft;
    private bool isRunning = false;
    private Action OnCompleteCallback;
    private Action<float> OnUpdateCallback;

    private float initialTime;

    public void StartTimer(float time, Action<float> OnUpdateCallback = null, Action OnCompleteCallback = null)
    {
        this.initialTime = time;
        this.OnUpdateCallback = OnUpdateCallback;
        this.OnCompleteCallback = OnCompleteCallback;
        this.timeLeft = time;
        isRunning = true;
    }

    public void AddTime(float time)
    {
        timeLeft += time;
    }

    public bool hasFinished()
    {
        return timeLeft <= 0 ? true : false;
    }

    private void Update()
    {
        if (isRunning)
        {
            timeLeft -= Time.deltaTime;
            CallUpdateCallback();
            CheckTimer();
        }
    }

    private void CallUpdateCallback()
    {
        if (OnUpdateCallback != null)
        {
            OnUpdateCallback(timeLeft);
        }
    }

    private void CheckTimer()
    {
        if (timeLeft <= 0)
        {
            OnCompleteTimer();
        }
    }

    private void OnCompleteTimer()
    {
        isRunning = false;
        timeLeft = 0;
        if (OnCompleteCallback != null)
        {
            OnCompleteCallback();
        }
    }

    public void ResetTimeAndStop()
    {
        timeLeft = initialTime;
        isRunning = false;
    }
}
