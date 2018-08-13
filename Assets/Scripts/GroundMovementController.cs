using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementController : MonoBehaviour {

	public Transform cam;
	float speed = 10;
    CharacterController mover;
	float accel = 11; 
	float turnSpeed ;
	public float turnSpeedLow;
	public float turnSpeedHigh;
	public float gravity ;
	Vector2 input;
	Vector3 camF;
	Vector3 camR;
	Vector3 intent;
	Vector3 velocity;
	Vector3 velocityXZ;
	bool grounded = true;
	public float jumpHeight;
    public float jumpHeightHold;
    float timer;
    bool jumpDown = false;


    void Start () {
		mover = GetComponent<CharacterController>();
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		DoInput();
		CalculateCamera();
		CalculateGround();
		DoMove();
		DoGravity();

        if (jumpDown)
        {
            this.timer += Time.deltaTime;
        }

        DoJump(this.timer);


        if (Input.GetButtonUp("Jump"))
        {
            jumpDown = false;
            print(this.timer);
            this.timer = 0;
        }

		mover.Move(velocity*Time.deltaTime);

		
	}

    private void DoMove()
    {
		intent = camF*input.y + camR*input.x;

		float ts = velocity.magnitude/5;
		turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, ts);
		if(input.magnitude > 0){
			Quaternion rot = Quaternion.LookRotation(intent);
			transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed*Time.deltaTime);
		}

		velocityXZ = velocity;
		velocityXZ.y = 0;

		velocityXZ = Vector3.Lerp(velocityXZ,transform.forward*input.magnitude*speed, accel*Time.deltaTime);
		velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
		
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

	private void DoGravity(){
		if(grounded){
			velocity.y = -0.5f;
		}else{
			velocity.y -= gravity * Time.deltaTime;
			velocity.y = Mathf.Clamp(velocity.y, -10, 10);
		}
	}

	private void CalculateGround(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position+Vector3.up*0.15f, -Vector3.up, out hit, 0.2f)){
			grounded = true;
		}else{
			grounded =false;
		}
	}

    private void DoJump(float timer){
		if(grounded){
            if (Input.GetButtonDown("Jump"))
            {
                jumpDown = true;
            }
            if (jumpDown)
            {
                if (timer < 0.1)
                {
                    print("tap");
                    velocity.y = jumpHeight;
                }
                else if (timer > 0.1)

                {
                    print("hold");
                    velocity.y = jumpHeightHold;
                }
            }

		}
	}
}
