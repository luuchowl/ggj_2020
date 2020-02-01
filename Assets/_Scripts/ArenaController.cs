using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ArenaController : MonoBehaviour
{
	[Tag] public string playerTag;
	public Animator doorAnim;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == playerTag)
		{
			ChangeDoorState(true);
		}
	}

	public void ChangeDoorState(bool closed)
	{
		doorAnim.SetBool("Closed", closed);
	}
}
