using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Hitbox hitBox;
	public AttackData attackData;
	public TrailRenderer trail;

	private void OnEnable()
	{
		hitBox.OpenCollision();
	}

	private void OnDisable()
	{
		trail.Clear();
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
			Transform fx = Game_Manager.instance.bulletDeathPool.GetPooledObject().transform;
			fx.position = transform.position;
			gameObject.SetActive(false);
		}
	}
}
