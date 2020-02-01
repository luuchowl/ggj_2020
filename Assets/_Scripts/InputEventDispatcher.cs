using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEventDispatcher : MonoBehaviour
{
    public UnityEvent onPress1;

    public UnityEvent onPress2;
    public UnityEvent onPress3;
    public UnityEvent onPress4;
    public UnityEvent onPressQ;
    public UnityEvent onPressW;
    public UnityEvent onPressE;
    public UnityEvent onPressR;
    public UnityEvent onPressA;
    public UnityEvent onPressS;
    public UnityEvent onPressD;
    public UnityEvent onPressF;
    public UnityEvent onPressZ;
    public UnityEvent onPressX;
    public UnityEvent onPressC;
    public UnityEvent onPressV;
    public UnityEvent onPressSpace;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            onPress1.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            onPress2.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            onPress3.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            onPress4.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            onPressQ.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            onPressW.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            onPressE.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            onPressR.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            onPressA.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            onPressS.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            onPressD.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            onPressF.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            onPressZ.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            onPressX.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            onPressC.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            onPressV.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onPressSpace.Invoke();
        }
    }
}