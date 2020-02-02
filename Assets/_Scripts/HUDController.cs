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
		HideAll();
		UpdateUI();
		StopAllCoroutines();
		StartCoroutine(ShowPlayerStatus_Routine());
	}

	private IEnumerator ShowPlayerStatus_Routine()
	{
		hudAnim.SetTrigger("ShowPlayerStatus");

		playerHealthHolder.SetActive(true);
		timerHolder.SetActive(true);
		skillsHolder.SetActive(true);

		yield return null;
	}

	public void ShowBossHealth()
	{
		bossHealthHolder.SetActive(true);
	}

	public void SetCurrentBoss(Enemy boss)
	{
		if(currentBoss != null)
		{
			currentBoss.damageEvent.RemoveListener(UpdateUI);
		}

		currentBoss = boss;
		currentBoss.damageEvent.AddListener(UpdateUI);

		hudAnim.SetTrigger("ShowBossHeath");
		UpdateUI();
	}
}
