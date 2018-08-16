using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour {

	

    private void OnCollisionEnter(Collision collision)
    {
        print("Collided!");
        if (collision.gameObject.tag == "Player")
        {
            print("Collided!");
            collision.gameObject.GetComponent<CharacterController>().Move(new Vector3(0, 500, 0));
        }

    }
}
