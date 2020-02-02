using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyingBullet : MonoBehaviour
{
	public Hitbox hitBox;
	public AttackData attackData;
	public ObjectPool bulletPool;
	public float angleVariation;
	public int maxCloneLevel = 2;

	[HideInInspector] public int currentCloneID;

	private void OnEnable()
	{
		hitBox.OpenCollision();
	}

	private void Update()
	{
		Collider[] collisions = hitBox.UpdateHitBox();

		if (collisions != null && collisions.Length > 0)
		{
			for (int i = 0; i < collisions.Length; i++)
			{
				IDamageable hit = collisions[i].GetComponent<IDamageable>();
				if(hit != null)
				{
					hit.ApplyDamage(hitBox, attackData);
				}
				else
				{
					if(currentCloneID < maxCloneLevel)
					{
						MultiplyingBullet b1 = bulletPool.GetPooledObject<MultiplyingBullet>();
						MultiplyingBullet b2 = bulletPool.GetPooledObject<MultiplyingBullet>();

						b1.currentCloneID = b2.currentCloneID = currentCloneID + 1;
						b1.bulletPool = b2.bulletPool = bulletPool;

						b1.transform.position = b2.transform.position = transform.position;
						b1.transform.rotation = b2.transform.rotation = transform.rotation;

						b1.transform.Rotate(0, 180 - angleVariation, 0);
						b2.transform.Rotate(0, 180 + angleVariation, 0);

						b1.transform.position += b1.transform.forward * 1;
						b2.transform.position += b2.transform.forward * 1;
					}
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
