using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
	public int maxHealth;
	public Hitbox contactHitbox;
	public AttackData contactDamageData;

	private int currentHealth;

	private void Awake()
	{
		currentHealth = maxHealth;
	}

	private void OnEnable()
	{
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
		Debug.Log("Morri");
	}
}
