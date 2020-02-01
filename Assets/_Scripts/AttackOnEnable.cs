using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class AttackOnEnable : MonoBehaviour
{
	public AttackData attackData;

	private Attack attackController;

	private void Awake()
	{
		attackController = GetComponent<Attack>();
	}

	private void OnEnable()
	{
		attackController.StartAttack(attackData);
	}
}
