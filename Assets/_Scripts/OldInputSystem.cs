using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldInputSystem : MonoBehaviour
{

    CharacterController controller;
    CharacterMovement movement;

    private Vector3 directionalInput = Vector3.zero;

    private Transform cameraDir;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraDir = Camera.main.transform;


        directionalInput = Vector3.zero;

        if (Input.GetKey(KeyCode.S))
        {
            directionalInput += new Vector3(0, 0, -1); 
        }
        if (Input.GetKey(KeyCode.W))
        {
            directionalInput += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            directionalInput += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            directionalInput += new Vector3(1, 0, 0);
        }

        Vector3 targetDirection = directionalInput;
        targetDirection = Camera.main.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;



        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && directionalInput != Vector3.zero)
        {
            movement.Dodge(targetDirection);
        }


        if (directionalInput != Vector3.zero)
        {
            movement.isWalking = true;
            movement.Walk(targetDirection.normalized);
        }
        else
        {
            movement.isWalking = false;
        }

        
        
    }
}
