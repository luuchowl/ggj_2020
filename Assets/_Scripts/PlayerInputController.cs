﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
	[Header("InputMap")]
	public PlayerInput playerInput;
	[Header("Movement")]
	public CharacterController controller;
	public CharacterMovement movement;
	[Header("Attack")]
	public float fireRate;
	public Transform gunBarrel;
	public LayerMask pointerMask;
	public PlayerStatus playerStatus;
	public Transform swordPosition;
	public Vector3 swordVariance;
	public float shootAngleVariance;
	[Header("Animations")]
    public Animator attackAnimator;
    public Animator characterAnimator;

    private Camera cam;
	private Vector3 moveDir;
	private Vector3 lookDir;
	private float reloadTime;
	private bool shooting;
	private Vector3 swordStartPos;
	
	private void Awake()
	{
		cam = Camera.main;
		swordStartPos = swordPosition.localPosition;
	}

	// Update is called once per frame
	void Update()
    {
		reloadTime += Time.deltaTime;

		if (moveDir != Vector3.zero)
		{
			movement.isWalking = true;
			movement.Walk(moveDir.normalized);
		}
		else
		{
			movement.isWalking = false;
		}

		if (lookDir.sqrMagnitude == 0 || !shooting)
		{
			movement.Look(moveDir.normalized);
		}
		else
		{
			movement.Look(lookDir.normalized);
		}

		if (shooting && reloadTime > 1 / fireRate) {
			reloadTime = 0;
			Transform bullet = Game_Manager.instance.playerBulletPool.GetPooledObject<Transform>();
			bullet.position = gunBarrel.position;
			bullet.rotation = gunBarrel.rotation;

			if (!playerStatus.upgradeShoot)
			{
				bullet.Rotate(Vector3.up, Random.Range(-shootAngleVariance, shootAngleVariance) * Timer.instance.GetNormalizedTime());
			}

            SoundManager.instance.PlayShoot1();
		}

        characterAnimator.SetFloat("WalkSpeed", movement.currentWalkVelocity / movement.walkSpeed);
        characterAnimator.SetBool("Shooting", shooting);
	}

	public void OnMove(InputValue value)
	{
		moveDir = value.Get<Vector2>();
		moveDir = cam.transform.TransformDirection(moveDir);
		moveDir.y = 0.0f;
	}

	public void OnLook(InputValue value)
	{
		if(playerInput.currentControlScheme == "Keyboard&Mouse")
		{
			if (shooting)
			{
				RaycastHit hit;

				if (Physics.Raycast(cam.ScreenPointToRay(value.Get<Vector2>()), out hit, Mathf.Infinity, pointerMask))
				{
					lookDir = hit.point - transform.position;
				}
			}
		}
		else
		{
			lookDir = value.Get<Vector2>();
			lookDir.z = lookDir.y;
			lookDir = cam.transform.TransformDirection(lookDir);
		}

		lookDir.y = 0;
	}

	public void OnFire(InputValue value)
	{
		shooting = value.Get<float>() > 0.5f;
	}

	public void OnAttack(InputValue value)
	{
		attackAnimator.SetTrigger("Attack");
        characterAnimator.SetTrigger("Attack");
        SoundManager.instance.PlayAttack();

		if (playerStatus.upgradeSword)
		{
			swordPosition.localPosition = swordStartPos;
		}
		else
		{
			Vector3 randomPos = new Vector3();
			randomPos.x = Random.Range(-swordVariance.x, swordVariance.x) * Timer.instance.GetNormalizedTime();
			randomPos.y = Random.Range(-swordVariance.y, swordVariance.y) * Timer.instance.GetNormalizedTime();
			randomPos.z = Random.Range(0, swordVariance.z) * Timer.instance.GetNormalizedTime();

			swordPosition.localPosition = randomPos;
		}
	}

	public void OnJump(InputValue value)
	{
		movement.Jump();
        SoundManager.instance.PlayJump();
    }

	public void OnRoll(InputValue value)
	{
		movement.Dodge(moveDir);
	}
}
