using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
	public float delay = 1;

	private void OnEnable()
	{
		Invoke("DisableObject", delay);
	}

	public void DisableObject()
	{
		gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		CancelInvoke("DisableObject");
	}
}
