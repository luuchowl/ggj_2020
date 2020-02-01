﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
	public float speed;
	
    // Update is called once per frame
    void Update()
    {
		transform.position += transform.forward * speed * Time.deltaTime;
    }
}
