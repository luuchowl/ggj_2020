using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[System.Serializable]
public class BoolEvent: UnityEvent<bool>
{

}

public class ArenaController : MonoBehaviour
{
	[Tag] public string playerTag;
	public Animator doorAnim;
	public BoolEvent doorChanged = new BoolEvent();

	private bool currentStatus;

	private void Awake()
	{
		Game_Manager.instance.gameStartEvent.AddListener(() => ChangeDoorState(false));
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == playerTag)
		{
			Debug.Log("aaaaa");
			ChangeDoorState(true);
		}
	}

	public void ChangeDoorState(bool closed)
	{
		Debug.Log(closed);
		if(closed == currentStatus)
		{
			return;
		}
		Debug.Log(2);
		doorAnim.SetBool("Closed", closed);
		doorChanged.Invoke(closed);

		currentStatus = closed;
	}
}
