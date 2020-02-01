using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloorManager : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] transforms;
    public int pattern = 2;
    // Start is called before the first frame update
    void Start()
    {
        transforms = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pattern == 0)
        {
            foreach(Transform child in transforms)
            {
                child.transform.position += new Vector3(0, Vector3.Distance(child.position, transform.position) * 0.005f * Mathf.Cos(Time.time), 0);
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
                    Mathf.PerlinNoise(child.transform.position.x + Time.time, child.transform.position.z + Time.time) * 10 - 10,
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
    }
}
