using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAMovement : MonoBehaviour
{
    public Vector3 targetPos;
    public float interpolationFactor = 0.002f;
    public Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
        pickRandomBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, interpolationFactor);
    }

    void pickRandomBehaviour()
    {
        StopAllCoroutines();
        int random = Random.Range(0, 1);
        if(random == 0)
        {
            StartCoroutine(CircularPath(Random.Range(-2f, 2f)));
        }
        if(random == 1)
        {
            StartCoroutine(CircularPath(Random.Range(-2f, 2f)));
        }
    }

    IEnumerator CircularPath(float speed)
    {
        float timer = 0;
        float duration = 4;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            targetPos = startPos + new Vector3(Mathf.Sin(Time.time * speed), 0, Mathf.Cos(Time.time * speed)) * 27f;
            yield return new WaitForEndOfFrame();
        }
        targetPos = startPos;
        yield return new WaitForSeconds(2f);
        pickRandomBehaviour();
    }

    IEnumerator StandStill(float duration)
    {
        targetPos = startPos;

        yield return new WaitForSeconds(duration);
        pickRandomBehaviour();
    }

    
}
