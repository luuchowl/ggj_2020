using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[System.Serializable]
public class AttackHitEvent : UnityEvent<Hitbox, AttackData> {
	
}

public class Attack : MonoBehaviour
{
	[Header("Preferences")]
	public Hitbox[] hitboxes;
	public AttackHitEvent hitEvent = new AttackHitEvent();
	[Header("Debug")]
	private Dictionary<Collider, Hitbox> hits = new Dictionary<Collider, Hitbox>();
    [ReadOnly] [SerializeField]
	private List<Collider> alreadyHit = new List<Collider>();

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
			Collider[] collisions = hitboxes[i].UpdateHitBox();

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

		foreach (KeyValuePair<Collider, Hitbox> item in hits)
		{
			if (!alreadyHit.Contains(item.Key))
			{
				item.Key.GetComponent<IDamageable>().ApplyDamage(item.Value, currentAttackData);
				alreadyHit.Add(item.Key);
				hitEvent.Invoke(item.Value, currentAttackData);
			}
		}
	}
}
