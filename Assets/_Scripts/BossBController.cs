using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class BossBController : MonoBehaviour
{
	public Transform nestCenter;
	public float nestRadius = 1;
	public LayerMask teleportObstacles;
	public Vector2 teleportDelay = Vector2.one;

	private Enemy enemyController;
	private Transform player;

	private void OnEnable()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		StartBattle();
	}

	public void StartBattle()
	{
		StartCoroutine(Battle_Routine());
	}

	private IEnumerator Battle_Routine()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(teleportDelay.x, teleportDelay.y));

			GetRandomPos();

			transform.position = GetRandomPos();
		}
	}

	public Vector3 GetRandomPos()
	{
		int tries = 100;
		Vector3 randomPos = new Vector3();
		randomPos.y = transform.position.y;
		Collider[] colliders = null;

		do
		{
			randomPos.x = nestCenter.position.x + Random.Range(-nestRadius, nestRadius);
			randomPos.z = nestCenter.position.z + Random.Range(-nestRadius, nestRadius);
			colliders = Physics.OverlapSphere(randomPos, 2, teleportObstacles);
		} while (tries > 0 && colliders.Length > 0); 

		if(tries == 0)
		{
			randomPos = transform.position;
		}

		return randomPos;
	}

	public void Die()
	{
		StopAllCoroutines();
		gameObject.SetActive(false);
	}

	private void OnDrawGizmosSelected()
	{
		if(nestCenter != null)
		{
			Gizmos.DrawWireSphere(nestCenter.position, nestRadius);
		}
	}
}
