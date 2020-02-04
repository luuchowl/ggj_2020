using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hitbox))]
public class EnableHitboxOnEnable : MonoBehaviour
{
	private Hitbox hitbox;

	private void Awake()
	{
		hitbox = GetComponent<Hitbox>();
	}

	private void OnEnable()
	{
		hitbox.OpenCollision();
	}

	private void OnDisable()
	{
		hitbox.CloseCollision();
	}
}
