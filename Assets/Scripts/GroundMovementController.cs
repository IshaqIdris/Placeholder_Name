using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementController : MonoBehaviour {
    
    //Public Manipulators
    public float turnSpeedLow;
    public float turnSpeedHigh;
    public float gravity;
	public Transform cam;
    public float jumpHeight;
    public float fallSpeed;
    public GameObject particles;
	
    CharacterController mover;

    float speed = 10;
	float accel = 11; 
	float turnSpeed ;
    float timer;
    float jumpCounter;
	
	Vector2 input;
	Vector3 camF;
	Vector3 camR;
	Vector3 intent;
	Vector3 velocity;
	Vector3 velocityXZ;

	bool grounded = true;
    bool jumpPad;
    bool jumpPadDown;
	

    bool jumpDown = false;

    bool cantDoubleJump;



    void Start () {
		mover = GetComponent<CharacterController>();
        timer = 0;
        jumpCounter = 0;
        cantDoubleJump = true;
        jumpPad = false;
        particles.SetActive(false);
        jumpPadDown = false;
	}
	
	// Update is called once per frame
	void Update () {
		DoInput();
		CalculateCamera();
		CalculateGround();
		DoMove();
		DoGravity();
        DoJump();

        if (Input.GetButtonUp("Jump"))
        {
            jumpDown = false;
        }

        print(velocity.magnitude);
        if(velocity.magnitude > 7){
            particles.SetActive(true);
        }else{
            particles.SetActive(false);
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
        }else if(jumpPad){
			velocity.y -= gravity * Time.deltaTime;
			velocity.y = Mathf.Clamp(velocity.y, -10, 10);
        }else{
            print("JUMPPAD");
            velocity.y -= gravity * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -30, 30);
        }
	}

	private void CalculateGround(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position+Vector3.up*0.15f, -Vector3.up, out hit, 0.2f)){
			grounded = true;
            jumpPadDown = false;
		}else{
			grounded =false;
		}
	}

    private void DoJump(){
        if (Input.GetButtonDown("Jump"))
        {
            jumpDown = true;
            print(jumpCounter);

            if (grounded)
            {
                if (jumpDown)
                {
                    velocity.y = jumpHeight;
                    jumpCounter = 0;
                    cantDoubleJump = false;
                }
            }
            else if (!grounded && jumpCounter < 1)
            {
                velocity.y = jumpHeight;
                jumpCounter += 1;
            }
        }
        else if (jumpPad)
        {
            velocity.y = 500;
            jumpPadDown = true;
            jumpPad = false;
        }

        else if ((mover.velocity.y < 0 && !jumpPad) ){
            velocity.y = -30;
        }else if (mover.velocity.y < 0 && jumpPad)
        {
            velocity.y = fallSpeed;
        }
	}

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        //Collision with Jump Pad
        //if(collision.gameObject.CompareTag("JumpPad")){
        //    jumpPad = true;
        //}else{
        //    jumpPad = false;
        //}

        //Collision with Moving Pad
        if (collision.gameObject.CompareTag("pad"))
        {
            print("On Rock");
            transform.parent = collision.transform;
        }else{
            print("DEPARENTED");
            transform.parent = null;
        }

        if (collision.gameObject.CompareTag("collectable")){
            collision.gameObject.SetActive(false);
        }
    }

    public void SetJumPad(bool jumpPadBool)
    {
        print("GOT HERE");
        jumpPad = jumpPadBool;
    }

    public CharacterController GetMover(){
        return mover;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("COLLIDED");
    }
}
