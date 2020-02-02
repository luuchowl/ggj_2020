using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PlayerStatus : MonoBehaviour, IDamageable
{
	public int maxHealth = 10;
	public int maxAttackStamina = 10;
	public int maxGunStamina = 10;

	public UnityEvent valuesChanged = new UnityEvent();
	public UnityEvent deathEvent = new UnityEvent();

	[ReadOnly] public int currentHealth;
	[ReadOnly] public int currentAttackStamina;
	[ReadOnly] public int currentGunStamina;

	private void OnEnable()
	{
		ResetStatus();
	}

	public void ApplyDamage(Hitbox hitbox, AttackData attackData)
	{
		currentHealth -= attackData.damage;
		valuesChanged.Invoke();

		if(currentHealth <= 0)
		{
			Debug.Log("Player Dead");
			deathEvent.Invoke();
		}
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
