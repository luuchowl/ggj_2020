using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class Enemy : MonoBehaviour, IDamageable
{
	public int maxHealth;
	public Hitbox contactHitbox;
	public AttackData contactDamageData;
	public UnityEvent damageEvent = new UnityEvent();
	public UnityEvent deathEvent = new UnityEvent();

	[ReadOnly] public int currentHealth;

	private void OnEnable()
	{
		currentHealth = maxHealth;
		contactHitbox.OpenCollision();
	}

	private void OnDisable()
	{
		contactHitbox.CloseCollision();
	}

	// Update is called once per frame
	void Update()
    {
		Collider[] collisions = contactHitbox.UpdateHitBox();

		if(collisions?.Length > 0)
		{
			collisions[0].GetComponent<IDamageable>().ApplyDamage(contactHitbox, contactDamageData);
			contactHitbox.OpenCollision();
		}
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
}
