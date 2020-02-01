using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour, IDamageable
{
	public Animator attackAnimator;
	public float fireRate;
	public Transform gunBarrel;
	public ObjectPool bulletPool;

	private float reloadTime;

	public void ApplyDamage(Hitbox hitbox, AttackData attackData)
	{
		Debug.Log("Jogador tomou dano");
	}

	// Update is called once per frame
	void Update()
    {
		reloadTime += Time.deltaTime;

		if (Input.GetMouseButtonDown(0))
		{
			attackAnimator.SetTrigger("Attack");
		}

		if (Input.GetMouseButton(1) && reloadTime > (1f / fireRate))
		{
			reloadTime = 0;
			Transform bullet = bulletPool.GetPooledObject<Transform>();
			bullet.position = gunBarrel.position;
			bullet.rotation = gunBarrel.rotation;
		}
	}
}
