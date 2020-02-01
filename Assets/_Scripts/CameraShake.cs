using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float frequency = 5;
    public float magnitude = 1;
    public bool translateX = true;
    public bool translateY = true;

    public AnimationCurve magnitudeOverTime = AnimationCurve.Constant(0, 1, 1);

    public void MakeCameraShake(float duration)
    {
        StartCoroutine(MakeCameraShakeRoutine(duration));
    }

    IEnumerator MakeCameraShakeRoutine(float duration)
    {
        Vector3 originalPos = transform.position;
        float timer = 0;
        float displacementX = 0;
        float displacementY = 0;
        while (timer < duration)
        {

            if (translateX)
            {
                displacementX = (Mathf.PerlinNoise(Time.time * frequency * 3, 0) * 2 - 1) * magnitude * magnitudeOverTime.Evaluate(timer / duration);
            }
            if (translateY)
            {
                displacementY = (Mathf.PerlinNoise(0, Time.time * frequency * 3) * 2 - 1) * magnitude * magnitudeOverTime.Evaluate(timer / duration);
            }


            transform.position = originalPos + new Vector3(displacementX, displacementY, 0);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = originalPos;

        yield return null;
    }
}
