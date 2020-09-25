using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Game_Manager : Singleton<Game_Manager>
{
	[HideInInspector] public Transform player;
	[HideInInspector] public PlayerInputController playerInput;
	[HideInInspector] public PlayerStatus playerStatus;

	public ObjectPool playerBulletPool;
	public ObjectPool enemyBulletPool;
	public ObjectPool swordHitPool;
	public ObjectPool bulletDeathPool;
	public ObjectPool explosionPool;

	public bool gameStarted { get; private set; }

	public UnityEvent gameStartEvent = new UnityEvent();
	public UnityEvent gameOverEvent = new UnityEvent();

	protected override void Awake()
	{
		base.Awake();
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		player = GameObject.FindGameObjectWithTag("Player")?.transform;

		if(player != null)
		{

			playerInput = FindObjectOfType<PlayerInputController>();
			playerStatus = FindObjectOfType<PlayerStatus>();

			playerStatus.deathEvent.RemoveListener(GameOver);
			playerStatus.deathEvent.AddListener(GameOver);
			playerInput?.playerInput.DeactivateInput();

		}
	}

	public void StartGame()
	{
		playerInput?.playerInput.ActivateInput();
		gameStartEvent.Invoke();
		Timer.instance.StartTimer();
		gameStarted = true;
	}

	public void GameOver()
	{
		playerInput?.playerInput.DeactivateInput();
		gameOverEvent.Invoke();
		gameStarted = false;

		SceneManager.LoadScene("GameOver");
	}
}
