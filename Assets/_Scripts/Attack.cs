using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Attack : MonoBehaviour
{
	public Hitbox[] hitboxes;
	[ReadOnly] [SerializeField]
	private Dictionary<Collider2D, Hitbox> hits = new Dictionary<Collider2D, Hitbox>();
    [ReadOnly] [SerializeField]
	private List<Collider2D> alreadyHit = new List<Collider2D>();

	private AttackData currentAttackData;

	public void StartAttack(AttackData data)
	{
		for (int i = 0; i < hitboxes.Length; i++)
		{
			hitboxes[i].OpenCollision();
		}

		hits.Clear();
		alreadyHit.Clear();
		currentAttackData = data;
	}

	public void EndAttack()
	{
		for (int i = 0; i < hitboxes.Length; i++)
		{
			hitboxes[i].CloseCollision();
		}

		hits.Clear();
		alreadyHit.Clear();
		currentAttackData = null;
	}

	private void Update()
	{
		for (int i = 0; i < hitboxes.Length; i++)
		{
			Collider2D[] collisions = hitboxes[i].UpdateHitBox();

			if(collisions == null)
			{
				continue;
			}

			for (int j = 0; j < collisions.Length; j++)
			{
				if (!hits.ContainsKey(collisions[j]))
				{
					hits.Add(collisions[j], hitboxes[i]);
				}
				else
				{
					//If the priority of the current hitbox is greater than the stored hitbox, we replace it in the dcitionary
					if (hitboxes[i].priority > hits[collisions[j]].priority)
					{
						hits[collisions[j]] = hitboxes[i];
					}
				}
			}
		}
	}

	private void LateUpdate()
	{
		foreach (KeyValuePair<Collider2D, Hitbox> item in hits)
		{
			if (!alreadyHit.Contains(item.Key))
			{
				item.Key.GetComponent<IDamageable>().ApplyDamage(item.Value, currentAttackData);
				alreadyHit.Add(item.Key);
			}
		}
	}
}
