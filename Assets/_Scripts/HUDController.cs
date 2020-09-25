using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
	public Animator hudAnim;
	public GameObject playerHealthHolder;
	public Image playerHealthBar;
	public GameObject bossHealthHolder;
	public Image bossHealthBar;
	public GameObject timerHolder;
	public TMP_Text timerText;
	public GameObject skillsHolder;
	public Image skillStaminaBar;
	public Image gunStaminaBar;

	private PlayerStatus player;
	private Enemy currentBoss;

	private void Awake()
	{
		player = FindObjectOfType<PlayerStatus>();
		player.valuesChanged.AddListener(UpdateUI);
		Game_Manager.instance.gameStartEvent.AddListener(ShowPlayerStatus);
	}

	private void Update()
	{
		if (Timer.instance == null)
		{
			Debug.Log("Time is null");
		}

		timerText.text = $"> TIME = {Timer.instance.totalTime - (Timer.instance.totalTime * Timer.instance.GetNormalizedTime()):0}";
	}

	public void UpdateUI()
	{
		playerHealthBar.fillAmount = (float)player.currentHealth / player.maxHealth;
		skillStaminaBar.fillAmount = (float)player.currentAttackStamina/ player.maxAttackStamina;
		gunStaminaBar.fillAmount = (float)player.currentGunStamina / player.maxGunStamina;

		if(currentBoss != null)
		{
			bossHealthBar.fillAmount = (float)currentBoss.currentHealth / currentBoss.maxHealth;
		}
	}

	public void HideAll()
	{
		playerHealthHolder.SetActive(false);
		bossHealthHolder.SetActive(false);
		timerHolder.SetActive(false);
		skillsHolder.SetActive(false);
	}

	public void HideBossHealth()
	{
		bossHealthHolder.SetActive(false);
	}

	public void ShowPlayerStatus()
	{
		playerHealthHolder.SetActive(true);
		timerHolder.SetActive(true);
		skillsHolder.SetActive(true);

		UpdateUI();
		hudAnim.SetTrigger("ShowPlayerStatus");
	}

	public void ShowBossHealth()
	{
		bossHealthHolder.SetActive(true);
		UpdateUI();
		hudAnim.SetTrigger("ShowBossHealth");
	}

	public void SetCurrentBoss(Enemy boss)
	{
		if(currentBoss != null)
		{
			currentBoss.damageEvent.RemoveListener(UpdateUI);
		}

		currentBoss = boss;
		currentBoss.damageEvent.AddListener(UpdateUI);

		hudAnim.SetTrigger("ShowBossHealth");
		UpdateUI();
	}
}
