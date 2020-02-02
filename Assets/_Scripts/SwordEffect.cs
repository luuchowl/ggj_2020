using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffect : MonoBehaviour
{
	public Attack attackControler;
	public Transform _hitbox;

	private void Awake()
	{
		attackControler.hitEvent.AddListener(SpawnFX);
	}

	private void SpawnFX(Hitbox hitBox, AttackData data)
	{
		Transform fx = Game_Manager.instance.swordHitPool.GetPooledObject().transform;
		fx.position = _hitbox.position;
	}
}
