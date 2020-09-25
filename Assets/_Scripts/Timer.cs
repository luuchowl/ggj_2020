using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{
    public Text text;
    public float totalTime = 120;
    private float currentTimer = 0;
    public bool complete = false;
    
    void Update()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= totalTime)
        {
            currentTimer = totalTime;
            complete = true;
        }

        text.text = Mathf.FloorToInt(totalTime - currentTimer).ToString();
    }

    public void StartTimer()
    {
        currentTimer = 0;
        complete = false;
    }

    public float GetNormalizedTime()
    {
        return (currentTimer / totalTime);
    }
}
