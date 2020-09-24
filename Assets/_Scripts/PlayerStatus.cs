using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PlayerStatus : MonoBehaviour, IDamageable
{
	public float invincibilityDuration = 1;
	public int maxHealth = 10;
	public int maxAttackStamina = 10;
	public int maxGunStamina = 10;
	public Animator playerAnim;

    [Header("Upgrades")]
    public bool upgradeSword;
    public bool upgradeShoot;
    public bool upgradeDodge;

	public UnityEvent valuesChanged = new UnityEvent();
	public UnityEvent deathEvent = new UnityEvent();

	[ReadOnly] public int currentHealth;
	[ReadOnly] public int currentAttackStamina;
	[ReadOnly] public int currentGunStamina;

	private bool invincible;

	private void OnEnable()
	{
		ResetStatus();
	}

	public void ApplyDamage(Hitbox hitbox, AttackData attackData)
	{
		if (invincible)
		{
			return;
		}

		currentHealth -= attackData.damage;
		SoundManager.instance.PlayDamage();

		valuesChanged.Invoke();

		if(currentHealth <= 0)
		{
			Debug.Log("Player Dead");
			deathEvent.Invoke();
		}
		else
		{
			StartCoroutine(Invencibility_Routine());

			if(currentHealth < (maxHealth * 0.2f))
			{
				SoundManager.instance.SetMusicMood(3);
			}
		}
	}

	private IEnumerator Invencibility_Routine()
	{
		SetInvincibility(true);
		yield return new WaitForSeconds(invincibilityDuration);
		SetInvincibility(false);
	}

	private void SetInvincibility(bool _invincible)
	{
		invincible = _invincible;
		playerAnim.SetBool("Damaged", invincible);
	}

	public void ResetStatus()
	{
		currentHealth = maxHealth;
		currentAttackStamina = maxAttackStamina;
		currentGunStamina = maxGunStamina;

		valuesChanged.Invoke();
	}

	public void OnAttack(InputValue value)
	{
		currentAttackStamina += 1;
		valuesChanged.Invoke();
	}
}
