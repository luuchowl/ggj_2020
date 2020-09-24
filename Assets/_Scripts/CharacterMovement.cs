using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
	public Transform model;

    //Walk
    public float walkSpeed = 10f;
    public float walkAcceleration = 0.1f;
    public float walkDeceleration = 0.9f;
    public float currentWalkVelocity = 0;

    //Run
    public float runSpeed = 25f;
    public float runAcceleration = 0.4f;
    private float runDeceleration = 0.9f;
    private float currentRunVelocity = 0;

    //Jump
    public float jumpHeight = 10f;
    public float gravity = -10;
    public float gravityMomentum = 0;

    //Acceleration to Walk
    //Jump (Free or locked)
    //Double jump
    //Wall jump
    public int numberOfJumps;
    private int currentJumpIndex;


    //Dodge & Dash // Coroutine
    public AnimationCurve dodgeVelocityOverTime = AnimationCurve.Constant(0, 1, 1);
    public AnimationCurve dodgeVelocityOverTimeRandom = AnimationCurve.Constant(0, 1, 1);
    public float dodgeDuration = 0.6f;
    public float dodgeSpeed = 20;
    public float dodgeRandomStart = 0;
    public float dodgeRandomEnd = 40;

    //Custom look direction
    //Moving platforms
    //SoftGround

    private CharacterController controller;
    private PlayerStatus status;

    public Vector3 lookDirection;
    public Vector3 movementDirection;
    public Vector3 targetMovementDirection;
    public float movementDirectionInterpolationFactor = 0.1f;

    public Vector3 gravityDirection = Vector3.down;

    public bool isWalking;
    public bool isRunning;
    public bool isJumping;
    public bool isGrounded;
    public bool isMovementLocked;
    public bool isOnDodge;

    public LayerMask groundLayer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        status = GetComponent<PlayerStatus>();
        lookDirection = transform.forward;
    }

    void Update()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, gravityDirection, out hitInfo, 1.1f, groundLayer))
            //(transform.position + gravityDirection * controller.height / 2, 0.1f, gravityDirection, out hitInfo, groundLayer))
        {
            isGrounded = true;
            gravityMomentum = 0;
        }
        else
        {
            isGrounded = false;
            gravityMomentum += gravity * Time.deltaTime;
        }
        
        

        if (!isGrounded)
        {
            controller.Move(new Vector3(0, gravityMomentum, 0));
        }


        if (!isOnDodge) { 

            if (isWalking) { currentWalkVelocity = Mathf.Lerp(currentWalkVelocity, walkSpeed, walkAcceleration); }
            if (!isWalking) {
                if (currentWalkVelocity < 0) {
                    currentWalkVelocity = 0;
                }
                currentWalkVelocity = Mathf.Lerp(currentWalkVelocity, 0, walkDeceleration);
            }
            if(currentWalkVelocity < 0.01) { currentWalkVelocity = 0; }

            if (isRunning) { currentRunVelocity = Mathf.Lerp(currentRunVelocity, runSpeed, runAcceleration); }
            if (!isRunning) { currentRunVelocity = Mathf.Lerp(currentRunVelocity, 0, runDeceleration); }

        
            if (currentWalkVelocity > 0) { controller.Move(movementDirection * currentWalkVelocity * Time.deltaTime); }
        }

        movementDirection = Vector3.Lerp(movementDirection, targetMovementDirection, movementDirectionInterpolationFactor);

        model.forward = lookDirection == Vector3.zero? model.forward : lookDirection;
    }

    public void Walk(Vector3 direction)
    {
        targetMovementDirection = direction;
    }

    public void Jump()
    {
		gravityMomentum = jumpHeight;
		controller.Move(gravityDirection * -0.1f);
        SoundManager.instance.PlaySwoosh();
    }

    public void Dodge(Vector3 direction)
    {
        if (!isOnDodge) { 
            StopAllCoroutines();
            StartCoroutine(DodgeRoutine(movementDirection));
            SoundManager.instance.PlayJump();
        }
    }

    public void Look(Vector3 direction)
    {
        lookDirection = direction.normalized;
    }

    public IEnumerator DodgeRoutine(Vector3 direction)
    {
        isOnDodge = true;

        float timer = 0;


        float randomDistance = 0;

        if (!status.upgradeDodge) { 
            randomDistance = dodgeSpeed + Random.Range(-Mathf.Lerp(dodgeRandomStart, dodgeRandomEnd, Timer.instance.GetNormalizedTime()), Mathf.Lerp(dodgeRandomStart, dodgeRandomEnd, Timer.instance.GetNormalizedTime()));
        }
    

        if (Timer.instance.GetNormalizedTime() < 0.85f || Random.Range(0, 4) == 0 || status.upgradeDodge)
        {
            //print(Timer.instance.GetNormalizedTime());
            while (timer < dodgeDuration)
            {
                timer += Time.deltaTime;
                controller.Move(direction * Time.deltaTime * dodgeVelocityOverTime.Evaluate(timer / dodgeDuration) * randomDistance);
                yield return new WaitForEndOfFrame();
            }


        }
        else
        {
            while (timer < dodgeDuration)
            {
                timer += Time.deltaTime;
                controller.Move(direction * Time.deltaTime * dodgeVelocityOverTimeRandom.Evaluate(timer / dodgeDuration) * randomDistance);
                yield return new WaitForEndOfFrame();
            }
        }

        

        currentWalkVelocity = 0;
        isOnDodge = false;
        //yield return null;
        //controller.Move(direction);

    }
}
