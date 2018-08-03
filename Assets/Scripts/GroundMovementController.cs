using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementController : MonoBehaviour {

	public Transform cam;
	float speed = 5;
    CharacterController mover;
	float accel = 2;
	float turnSpeed = 5;
	Vector2 input;
	Vector3 camF;
	Vector3 camR;
	Vector3 intent;
	Vector3 velocity;

    void Start () {
		mover = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		DoInput();
		CalculateCamera();
		DoMove();
		
	}

    private void DoMove()
    {
		intent = camF*input.y + camR*input.x;

		if(input.magnitude > 0){
			Quaternion rot = Quaternion.LookRotation(intent);
			transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed*Time.deltaTime);
		}

		velocity = Vector3.Lerp(velocity,transform.forward*input.magnitude*speed, accel*Time.deltaTime);
		mover.Move(velocity*Time.deltaTime);
    }

    private void CalculateCamera()
    {
        camF=cam.forward;

		camR=cam.right;

		camF.y = 0;
		camR.y = 0;
		camF = camF.normalized;
		camR = camR.normalized;
    }

    private void DoInput()
    {
		input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = Vector2.ClampMagnitude(input,1);
    }
}
