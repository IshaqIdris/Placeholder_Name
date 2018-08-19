using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {

    public float shootSpeed = 10;

    GameObject bullet;
    float timer;


    // Use this for initialization
    void Start()
    {
        timer = 0;
        bullet = Resources.Load("Boulder") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Time code each bullet is shot
        if (timer > 3)
        {
            shoot(transform.forward);
            timer = 0;
        }
    }

    //Instaniates Bullet and shoots in the forward direction
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

