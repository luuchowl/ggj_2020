using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text text;
    public float totalTime = 120;
    private float currentTimer = 0;
    
    void Start()
    {
        StartTimer();
    }
    
    void Update()
    {
        currentTimer += Time.deltaTime;
        text.text = Mathf.FloorToInt(totalTime - currentTimer).ToString();
    }

    public void StartTimer()
    {
        currentTimer = 0;
    }
}
