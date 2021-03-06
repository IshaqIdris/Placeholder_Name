﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    GameObject bullet;
    float timer;
    public float shootSpeed = 10;
    float bigBulletTime;


	// Use this for initialization
	void Start () {
        timer = 0;
        bigBulletTime = 0;
        bullet = Resources.Load("bullet") as GameObject;
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        bigBulletTime += Time.deltaTime;

        if (timer > 0.5)
        {
            //Bullets shot in different directions
            shoot(transform.forward);
            shoot(Quaternion.Euler(0, -45, 0) * new Vector3(0f, 0f, -1f));
            shoot(Quaternion.Euler(0, 45, 0) * new Vector3(0f, 0f, -1f));
            shoot(Quaternion.Euler(0, 135, 0) * new Vector3(0f, 0f, -1f));
            shoot(Quaternion.Euler(0, -135, 0) * new Vector3(0f, 0f, -1f));
            shoot(Quaternion.Euler(0, -75, 0) * new Vector3(0f, 0f, -1f));
            shoot(Quaternion.Euler(0, 75, 0) * new Vector3(0f, 0f, -1f));
            shoot(new Vector3(1f, 0f, 0f));
            shoot(new Vector3(-1f, 0f, -0f));

            timer = 0;
        }
	}

    private void shoot(Vector3 forward)
    {
        GameObject projectileForward = Instantiate(bullet) as GameObject;
        projectileForward.transform.position = transform.position + forward;
        Rigidbody rb = projectileForward.GetComponent<Rigidbody>();

        //Fire big bullet every <X> seconds
        if (bigBulletTime > 1)
        {
            projectileForward.transform.localScale += new Vector3(1F, 1F, 1F);
            bigBulletTime = 0;
            rb.velocity = forward * shootSpeed * 10;
        }else{
            rb.velocity = forward * shootSpeed;
        }
        Destroy(projectileForward, 10);
    }
}
