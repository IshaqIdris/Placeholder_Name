using UnityEngine;
using System.Collections;

public class EnemyBoulder: MonoBehaviour
{
    // Use this for initialization
    private void O(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Set player to dead
            GetComponent<Animation>().setDead(true);
        }
    }
}
