using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadLow : MonoBehaviour
{



    // Use this for initialization
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("TRIGGERED");
            other.gameObject.GetComponent<GroundMovementController>().SetJumPad(true, "low");
        }
    }

}
