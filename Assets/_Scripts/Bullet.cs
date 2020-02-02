using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Hitbox hitBox;
	public AttackData attackData;
	public GameObject destroyFX;

	private void OnEnable()
	{
		hitBox.OpenCollision();
	}

	private void Update()
	{
		Collider[] collisions = hitBox.UpdateHitBox();

		if(collisions != null && collisions.Length > 0)
		{
			for (int i = 0; i < collisions.Length; i++)
			{
				IDamageable hit = collisions[i].GetComponent<IDamageable>();

				if(hit != null)
				{
					hit.ApplyDamage(hitBox, attackData);
				}

				break;
			}

			hitBox.CloseCollision();
			gameObject.SetActive(false);
		}
	}
}
