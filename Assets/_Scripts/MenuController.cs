using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
	{
		Game_Manager.instance.StartGame();
		gameObject.SetActive(false);
	}
}
