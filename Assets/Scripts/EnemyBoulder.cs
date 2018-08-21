using UnityEngine;
using System.Collections;

public class EnemyBoulder: MonoBehaviour
{
    // Use this for initialization
    private void O(Collider other)
    {
        print("BOULDER Hit");
        if (other.gameObject.CompareTag("Player"))
        {
            print("BOULDER");
            GetComponent<Animation>().setDead(true);
        }
    }
}
