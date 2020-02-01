using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
	public Animator attackAnimator;
	public float fireRate;
	public Transform gunBarrel;
	public ObjectPool bulletPool;

	private float reloadTime;

	private void Start()
	{
		
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
