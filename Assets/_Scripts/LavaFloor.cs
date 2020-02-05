using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloor : MonoBehaviour
{
	public AttackData lavaDamageData;
	public Hitbox hitbox;
	public Transform respawnPoint;

	private void Update()
	{
		Collider[] col = hitbox.UpdateHitBox();

		if(col != null && col.Length > 0)
		{
			col[0].GetComponent<IDamageable>().ApplyDamage(hitbox, lavaDamageData);
			hitbox.OpenCollision();
		}
	}
}
