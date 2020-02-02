using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerStatus : MonoBehaviour, IDamageable
{
	public int maxHealth = 10;
	public int maxAttackStamina = 10;
	public int maxGunStamina = 10;

	public UnityEvent damageEvent = new UnityEvent();
	public UnityEvent deathEvent = new UnityEvent();

	private int currentHealth;
	private int currentAttackStamina;
	private int currentGunStamina;

	private void OnEnable()
	{
		ResetStatus();
	}

	public void ApplyDamage(Hitbox hitbox, AttackData attackData)
	{
		if(currentHealth <= 0)
		{
			deathEvent.Invoke();
		}
		else
		{
			currentHealth -= attackData.damage;
			damageEvent.Invoke();
		}
	}

	public void ResetStatus()
	{
		currentHealth = maxHealth;
		currentAttackStamina = maxAttackStamina;
		currentGunStamina = maxGunStamina;
	}

	public void OnAttack(InputValue value)
	{
		currentAttackStamina += 1;
	}
}
