using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public Animator menuAnim;
	public Vector2 glitchInterval = Vector2.one;

    public void StartGame()
	{
		Game_Manager.instance.StartGame();
        SoundManager.instance.SetMusicMood(1);
		gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		StartCoroutine(Glicth_Routine());
	}

	private IEnumerator Glicth_Routine()
	{
		while (true)
		{
			menuAnim.SetTrigger("Glitch");

			yield return new WaitForSeconds(Random.Range(glitchInterval.x, glitchInterval.y));
		}
	}

	private void Update()
	{
		if(!Game_Manager.instance.gameStarted)
		{
			if (Keyboard.current.anyKey.wasPressedThisFrame ||
				Mouse.current.leftButton.wasPressedThisFrame ||
				Mouse.current.rightButton.wasPressedThisFrame ||
				Gamepad.current.allControls.Any(x => x is ButtonControl && x.IsPressed() && !x.synthetic))
			{
				StartGame();

			}
		}
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}
