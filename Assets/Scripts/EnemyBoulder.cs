using UnityEngine;
using System.Collections;

public class EnemyBoulder: MonoBehaviour
{
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("BOULDER");
            other.gameObject.GetComponent<Animation>().setDead(true);
        }
    }
}
