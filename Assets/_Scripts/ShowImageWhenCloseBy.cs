using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImageWhenCloseBy : MonoBehaviour
{
	public float distance = 1;
	public GameObject imageToShow;
	public Transform icon;
	public float movMagnitude;
	public float movFrequency;

	private bool started;

	private void Start()
	{
		Game_Manager.instance.gameStartEvent.AddListener(() => started = true);
	}

	// Update is called once per frame
	void Update()
    {
		icon.localPosition = Vector3.up * (Mathf.Sin(Time.time * movFrequency) * movMagnitude);

		if (started && Game_Manager.instance.player != null && Vector3.Distance(transform.position, Game_Manager.instance.player.position) < distance)
		{
			imageToShow.SetActive(true);
		}
		else
		{
			imageToShow.SetActive(false);
		}
	}
}
