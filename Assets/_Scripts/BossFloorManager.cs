using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloorManager : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] transforms;
    public int pattern = 2;
    public float yHeight;

    public float routineADuration;
    public float routineAIntensity;
    public AnimationCurve routineAIntensityOverTime;

    public float routineBDuration;
    public float routineBIntensity;
    public AnimationCurve routineBIntensityOverTime;

    public float routineCDuration;
    public float routineCIntensity;
    public AnimationCurve routineCIntensityOverTime;

    public float routineDDuration;
    public float routineDIntensity;
    public AnimationCurve routineDIntensityOverTime;
    
    void Start()
    {
        //transforms = GetComponentsInChildren<Transform>();

        transforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            transforms[i] = transform.GetChild(i);
        }


        yHeight = transforms[5].localPosition.y;
        print(yHeight + " " + transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (pattern == 0)
        {
            foreach(Transform child in transforms)
            {
                child.transform.localPosition = new Vector3(
                    child.transform.localPosition.x,
                    -0.5f  + Vector3.Distance(child.position, transform.position) * 0.05f * Mathf.Cos(Time.time),
                    child.transform.localPosition.z
                    );
                /*child.transform.position = new Vector3(
                    child.transform.position.x,
                    yHeight + Vector3.Distance(child.position, transform.position) * 0f * Mathf.Cos(Time.time),
                    child.transform.position.z);
                    */
            }
        }
        if (pattern == 1)
        {
            foreach (Transform child in transforms)
            {
                //child.transform.position += new Vector3(0, Vector3.Distance(child.position, transform.position) * 0.005f * Mathf.Cos(Time.time), 0);
                child.transform.position = new Vector3(
                    child.transform.position.x,
                    child.transform.position.y,
                    child.transform.position.z
                    );
            }
        }
        if (pattern == 2)
        {
            foreach (Transform child in transforms)
            {
                //child.transform.position += new Vector3(0, Vector3.Distance(child.position, transform.position) * 0.005f * Mathf.Cos(Time.time), 0);
                child.transform.position = new Vector3(
                    child.transform.position.x,
                    -0.5f + Mathf.PerlinNoise(child.transform.position.x + Time.time, child.transform.position.z + Time.time) * 10,
                    child.transform.position.z
                    );
            }
        }

        if(pattern == 3)
        {
            foreach(Transform child in transforms)
            {
                child.transform.position = new Vector3(
                    child.transform.position.x,
                    Vector3.Dot(Vector3.forward, child.transform.position -  transform.position),
                    child.transform.position.z
                    );
            }
        }
        if (pattern == 4)
        {
            foreach (Transform child in transforms)
            {
                child.transform.position = new Vector3(
                    child.transform.position.x,
                    Mathf.Clamp01(Vector3.Dot(new Vector3(Mathf.Sin(Time.time), 0, Mathf.Cos(Time.time)), (child.transform.position - transform.position).normalized))* -50,
                    child.transform.position.z
                    );
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            StopAllCoroutines();
            StartCoroutine(AttackRoutineA());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StopAllCoroutines();
            StartCoroutine(AttackRoutineB());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StopAllCoroutines();
            StartCoroutine(AttackRoutineC());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            StopAllCoroutines();
            StartCoroutine(AttackRoutineD());
        }
    }

    public void PickRandomBehavior()
    {
        StopAllCoroutines();
        SoundManager.instance.PlayJumpBass();
        int random = Random.Range(1, 5);
        switch (random)
        {
            case 0:
                StartCoroutine(AttackRoutineA());
                break;
            case 1:
                StartCoroutine(AttackRoutineB());
                break;
            case 2:
                StartCoroutine(AttackRoutineC());
                break;
            case 3:
                StartCoroutine(AttackRoutineD());
                break;
            case 4:
                StartCoroutine(AttackRoutineE());
                break;
        }
    }

    IEnumerator AttackRoutineA()
    {
        float timer = 0;

        while(timer < routineADuration)
        {
            timer = timer + Time.deltaTime;
            foreach (Transform child in transforms)
            {
                child.transform.localPosition = new Vector3(
                        child.transform.localPosition.x,
                        yHeight+ Mathf.Clamp01(Vector3.Dot(new Vector3(Mathf.Sin(Time.time), 0, Mathf.Cos(Time.time)), (child.transform.position - transform.position).normalized)) * routineAIntensity * routineAIntensityOverTime.Evaluate(timer/routineADuration),
                        child.transform.localPosition.z
                    );
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.6f);
        PickRandomBehavior();
    }

    IEnumerator AttackRoutineB()
    {
        float timer = 0;

        float seed = Random.Range(0f, 10f);

        while (timer < routineBDuration)
        {
            timer = timer + Time.deltaTime;
            
            foreach (Transform child in transforms)
            {
                child.transform.localPosition = new Vector3(
                    child.transform.localPosition.x,
                    yHeight + routineBIntensityOverTime.Evaluate(timer / routineBDuration) * Mathf.Clamp01(Mathf.Pow(Mathf.PerlinNoise(
                            child.transform.localPosition.x + seed * 50 ,
                            child.transform.localPosition.z + seed * 50 ), 10)) * routineBIntensity, //* routineBIntensity ,
                    child.transform.localPosition.z
                    );
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.6f);
        PickRandomBehavior();
    }

    IEnumerator AttackRoutineC()
    {
        
        float timer = 0;

        float seed = Random.Range(0f, 10f);

        while (timer < routineCDuration)
        {
            timer = timer + Time.deltaTime;
            foreach (Transform child in transforms)
            {
                child.transform.localPosition = new Vector3(
                    child.transform.localPosition.x,
                    yHeight + Mathf.Pow(Mathf.Clamp01(Mathf.Sin(Vector3.Distance(new Vector3(child.position.x, child.position.z), new Vector3(transform.position.x, transform.position.z)) * 0.05f)),     10) * routineCIntensity * routineCIntensityOverTime.Evaluate(timer/routineCDuration),
                    child.transform.localPosition.z
                    );
                /*child.transform.position = new Vector3(
                    child.transform.position.x,
                    yHeight + Vector3.Distance(child.position, transform.position) * 0f * Mathf.Cos(Time.time),
                    child.transform.position.z);
                    */
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.6f);
        PickRandomBehavior();
    }

    IEnumerator AttackRoutineD()
    {
        //print("d");
        float timer = 0;

        float seed = Random.Range(0f, 10f);

        while (timer < routineDDuration)
        {
            timer = timer + Time.deltaTime;
            foreach (Transform child in transforms)
            {
                child.transform.localPosition = new Vector3(
                    child.transform.localPosition.x,
                    yHeight + Mathf.Pow( 1 - Mathf.Clamp01(Mathf.Sin(Vector3.Distance(new Vector3(child.position.x, child.position.z) * 2, new Vector3(transform.position.x, transform.position.z)) * 0.05f)), 10) * routineDIntensity * routineDIntensityOverTime.Evaluate(timer / routineDDuration),
                    //yHeight + Mathf.Pow(1 - Mathf.Clamp01(Mathf.Sin(Vector3.Distance(new Vector3(child.position.x, child.position.z) * 1.5f, new Vector3(transform.position.x, transform.position.z)) * 0.05f)), 10) * routineCIntensity * routineCIntensityOverTime.Evaluate(timer / routineCDuration),
                    //yHeight + Mathf.Pow(1 - Mathf.Clamp01(Mathf.Sin(Vector3.Distance(new Vector3(child.position.x, child.position.z), new Vector3(transform.position.x, transform.position.z)) * 0.05f + 0.01f)), 3 ) * routineCIntensity * routineCIntensityOverTime.Evaluate(timer / routineCDuration),
                    child.transform.localPosition.z
                    );
                /*child.transform.position = new Vector3(
                    child.transform.position.x,
                    yHeight + Vector3.Distance(child.position, transform.position) * 0f * Mathf.Cos(Time.time),
                    child.transform.position.z);
                    */
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.6f);
        PickRandomBehavior();
    }

    IEnumerator AttackRoutineE()
    {
        float timer = 0;

        while (timer < routineADuration)
        {
            timer = timer + Time.deltaTime;
            foreach (Transform child in transforms)
            {
                child.transform.localPosition = new Vector3(
                        child.transform.localPosition.x,
                        yHeight + Mathf.Clamp01(Vector3.Dot(new Vector3(Mathf.Sin(-Time.time), 0, Mathf.Cos(-Time.time)), (child.transform.position - transform.position).normalized)) * routineAIntensity * routineAIntensityOverTime.Evaluate(timer / routineADuration),
                        child.transform.localPosition.z
                    );
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.6f);
        PickRandomBehavior();
    }

    public void ResetPositions()
	{
        StopAllCoroutines();
        StartCoroutine(ResetPositionsRoutine());
	}

    private IEnumerator ResetPositionsRoutine()
	{
        float duration = 1f;
        float t = 0;

		while (t < duration)
		{
            t += Time.deltaTime;

			foreach (Transform child in transform)
			{

                Vector3 targetPos = child.localPosition;
                targetPos.y = 0;
                child.localPosition = Vector3.Lerp(child.localPosition, targetPos, t / duration);
			}

            yield return null;
		}
    }
}
