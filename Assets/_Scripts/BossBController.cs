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
	public Animator bossAnim;

	private Enemy enemyController;
	private Transform player;

	private void Awake()
	{
		enemyController = GetComponent<Enemy>();
		enemyController.deathEvent.AddListener(Die);
		enemyController.damageEvent.AddListener(PlayDamageAnim);
	}

	private void OnEnable()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public void StartBattle()
	{
		StartCoroutine(Battle_Routine());
		SoundManager.instance.SetMusicMood(2);
	}

	private IEnumerator Battle_Routine()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(teleportDelay.x, teleportDelay.y));

			bossAnim.SetTrigger("TeleportOut");
			yield return new WaitForSeconds(1);

			transform.position = GetRandomPos();

			bossAnim.SetTrigger("TeleportIn");
			transform.forward = (player.position - transform.position);
			ShootBullet();
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

	public void ShootBullet()
	{
		MultiplyingBullet b = Game_Manager.instance.enemyBulletPool.GetPooledObject<MultiplyingBullet>();
		b.currentCloneID = 0;
		b.bulletPool = Game_Manager.instance.enemyBulletPool;
		b.transform.position = transform.position;
		b.transform.forward = player.position - transform.position;
	}

	public void Die()
	{
		StopAllCoroutines();
		Game_Manager.instance.enemyBulletPool.ReturnAllObjectsToPool();
		SoundManager.instance.SetMusicMood(1);
		Transform fx = Game_Manager.instance.explosionPool.GetPooledObject().transform;
		fx.position = transform.position;
		gameObject.SetActive(false);
	}

	private void PlayDamageAnim()
	{
		bossAnim.SetTrigger("Damaged");
	}

	private void OnDrawGizmosSelected()
	{
		if(nestCenter != null)
		{
			Gizmos.DrawWireSphere(nestCenter.position, nestRadius);
		}
	}
}
