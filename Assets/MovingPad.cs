using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPad : MonoBehaviour
{


    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent = collision.transform;
        }
    }
}
