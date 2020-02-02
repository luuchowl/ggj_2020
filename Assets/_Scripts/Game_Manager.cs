﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Game_Manager : Singleton<Game_Manager>
{
	[HideInInspector] public Transform player;
	[HideInInspector] public PlayerInputController playerInput;
	[HideInInspector] public PlayerStatus playerStatus;

	public UnityEvent gameStartEvent = new UnityEvent();
	public UnityEvent gameOverEvent = new UnityEvent();

	private void Awake()
	{
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerInput = FindObjectOfType<PlayerInputController>();
		playerStatus = FindObjectOfType<PlayerStatus>();

		playerStatus.deathEvent.RemoveListener(GameOver);
		playerStatus.deathEvent.AddListener(GameOver);
		playerInput?.playerInput.DeactivateInput();
	}

	public void StartGame()
	{
		playerInput?.playerInput.ActivateInput();
		gameStartEvent.Invoke();
	}

	public void GameOver()
	{
		playerInput?.playerInput.DeactivateInput();
		gameOverEvent.Invoke();

		//TODO
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
