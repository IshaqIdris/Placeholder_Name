using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {

    GameObject bullet;
    float timer;
    public float shootSpeed = 10;
    float bigBulletTime;


    // Use this for initialization
    void Start()
    {
        timer = 0;
        bigBulletTime = 0;
        bullet = Resources.Load("Boulder") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        bigBulletTime += Time.deltaTime;

        if (timer > 3)
        {
            // = this.spiral * Quaternion.Euler(0, 10, 0);
            shoot(transform.forward);
            //shoot(Quaternion.Euler(0, -45, 0) * new Vector3(0f, 0f, -1f));
            //shoot(Quaternion.Euler(0, 45, 0) * new Vector3(0f, 0f, -1f));
            //shoot(Quaternion.Euler(0, 135, 0) * new Vector3(0f, 0f, -1f));
            //shoot(Quaternion.Euler(0, -135, 0) * new Vector3(0f, 0f, -1f));
            //shoot(Quaternion.Euler(0, -75, 0) * new Vector3(0f, 0f, -1f));
            //shoot(Quaternion.Euler(0, 75, 0) * new Vector3(0f, 0f, -1f));
            //shoot(new Vector3(1f, 0f, 0f));
            //shoot(new Vector3(-1f, 0f, -0f));

            timer = 0;
        }



    }

    private void shoot(Vector3 forward)
    {
        GameObject projectileForward = Instantiate(bullet) as GameObject;
        projectileForward.transform.position = transform.position + forward;
        Rigidbody rb = projectileForward.GetComponent<Rigidbody>();


        projectileForward.transform.localScale += new Vector3(1F, 1F, 1F);
       
        rb.velocity = forward * shootSpeed;

        Destroy(projectileForward, 2);
    }
}

