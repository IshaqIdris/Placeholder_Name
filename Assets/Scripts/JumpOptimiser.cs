using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOptimiser : MonoBehaviour
{

    private CharacterController mover;
    public float fallMultiplier = 10;

    void Start()
    {
        mover = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(mover.velocity.y < 0){
            //mover.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            print("Falling");
        }
    }

}