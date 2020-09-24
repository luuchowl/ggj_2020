using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectsManager : MonoBehaviour
{
    public float digitalGlitchBegin = 0;
    public float digitalGlitchEnd = 1;
    public float digitalGlitchVariance = 0.02f;
    public float digitalGlitchFrequency = 0.3f;
    public float digitalGlitchDefault = -0.3f;
    private float currentDigitalGlitch;
   

    private Kino.AnalogGlitch analogGlitch;
    private Kino.DigitalGlitch digitalGlitch;

    private void Start()
    {
        digitalGlitch = GetComponent<Kino.DigitalGlitch>();
        analogGlitch = GetComponent<Kino.AnalogGlitch>();
        StartCoroutine(GlitchOverTimeRoutine());
    }

    IEnumerator GlitchOverTimeRoutine()
    {
        //print("coroutine");
        currentDigitalGlitch = digitalGlitchBegin;
        while (!Timer.instance.complete)
        {
            currentDigitalGlitch = Timer.instance.GetNormalizedTime() * digitalGlitchEnd + Mathf.Sin(Time.time * digitalGlitchFrequency) * digitalGlitchVariance + digitalGlitchDefault; 
            //currentDigitalGlitch = Mathf.Sin(Time.time * digitalGlitchFrequency) * digitalGlitchVariance;
            digitalGlitch.intensity = currentDigitalGlitch;
            analogGlitch.colorDrift = Mathf.Clamp01(currentDigitalGlitch* 1);


            yield return new WaitForEndOfFrame();
            

        }
        yield return null;
    }
}
