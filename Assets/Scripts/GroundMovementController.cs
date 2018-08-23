using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementController : MonoBehaviour
{

    //Public Manipulators
    public float turnSpeedLow;
    public float turnSpeedHigh;
    public float gravity;
    public Transform cam;
    public float jumpHeight;
    public float fallSpeed;
    public GameObject particles;
    public GameObject respawnPoint;

    //Player character controller
    CharacterController mover;

    float speed = 10;
    float accel = 11;
    float turnSpeed;
    float timer;
    float jumpCounter;
    float speedBoost;

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

    String jumpPadType;

    void Start()
    {
        speedBoost = 0;
        mover = GetComponent<CharacterController>();
        timer = 0;
        jumpCounter = 0;
        cantDoubleJump = true;
        jumpPad = false;
        particles.SetActive(false);
        jumpPadDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        DoInput();
        CalculateCamera();
        CalculateGround();
        DoMove();
        DoGravity();
        DoJump();
        CheckSpeed();


        if (Input.GetButtonUp("Jump"))
        {
            jumpDown = false;
        }

        //player trail
        if (velocity.magnitude > 7)
        {
            particles.SetActive(true);
        }
        else
        {
            particles.SetActive(false);
        }
        mover.Move(velocity * Time.deltaTime);
    }

    //Limit speed boosts by collectables
    void CheckSpeed(){
        if(speed>10){
            speedBoost += Time.deltaTime;
            if(speedBoost > 1){
                speed = 10;
                speedBoost = 0;
            }
        }
    }

    //excecute player movements
    private void DoMove()
    {
        intent = camF * input.y + camR * input.x;

        float ts = velocity.magnitude / 5;
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, ts);
        if (input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        velocityXZ = velocity;
        velocityXZ.y = 0;

        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * input.magnitude * speed, accel * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);

    }

    private void CalculateCamera()
    {
        camF = cam.forward;
        camR = cam.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    //get inputs from controller
    private void DoInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
    }

    //Calculate gravity to be applied to the player
    private void DoGravity()
    {
        if (grounded)
        {
            velocity.y = -0.5f;
        }
        else if (jumpPad)
        {
            velocity.y -= gravity * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -10, 10);
        }

        //Gravity affected by what jumpad interacted with
        else if (jumpPadType == "low")
        {
            velocity.y -= gravity * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -20, 20);
        }
        else if (jumpPadType == "medium"){
            velocity.y -= gravity * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -30, 30);
        }
        else if (jumpPadType =="high")
        {
            velocity.y -= gravity * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -60, 60);
        }else{
            velocity.y -= gravity * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -30, 30);
        }
    }

    //Detect if player grounded
    private void CalculateGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.15f, -Vector3.up, out hit, 0.2f))
        {
            grounded = true;
            jumpPadDown = false;
        }
        else
        {
            grounded = false;
        }
    }

    //Perform jumping mechanic
    private void DoJump()
    {
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

        //Different jump velocity speeds with jumpad interaction
        else if (jumpPad && jumpPadType == "low" )
        {
            velocity.y = 50;
            jumpPadDown = true;
            jumpPad = false;
        }else if (jumpPad && jumpPadType == "medium")
        {
            velocity.y = 70;
            jumpPadDown = true;
            jumpPad = false;
        }else if (jumpPad && jumpPadType == "high")
        {
            velocity.y = 500;
            jumpPadDown = true;
            jumpPad = false;
        }

        //Different velocity fall down speeds based on jumpad interaction
        else if ((mover.velocity.y < 0 && !jumpPad && jumpPadDown && jumpPadType == "low"))
        {
            velocity.y = -20;
        }
        else if ((mover.velocity.y < 0 && !jumpPad && jumpPadDown && jumpPadType == "high"))
        {
            velocity.y = -50;
        }
        else if ((mover.velocity.y < 0 && !jumpPad && jumpPadDown && jumpPadType == "medium"))
        {
            velocity.y = -30;
        }
        else if (mover.velocity.y < 0 )
        {
            velocity.y = fallSpeed;
        }
    }


    void OnControllerColliderHit(ControllerColliderHit collision)
    {

        //Collision with Moving Pad
        if (collision.gameObject.CompareTag("pad"))
        {
            transform.parent = collision.transform;
        }
        else
        {
            transform.parent = null;
        }

        //Collision with collectable with speed boost
        if (collision.gameObject.CompareTag("collectable"))
        {
            collision.gameObject.SetActive(false);
            if (speed < 19)
            {
                speed += 6;
            }
        }

        //collision with non speed boost collectable
        if (collision.gameObject.CompareTag("collectable2"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    //If player has interacted with jumpad
    public void SetJumPad(bool jumpPadBool, String type)
    {
        jumpPad = jumpPadBool;
        jumpPadType = type;
    }

    //Return character controller
    public CharacterController GetMover()
    {
        return mover;
    }

    //Player collision with boulder
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("boulder"))
        {
            mover.GetComponentInChildren<Animation>().setDead(true);
            mover.transform.position = respawnPoint.transform.position;
            print(respawnPoint.transform.position.ToString());
        }
    }
}