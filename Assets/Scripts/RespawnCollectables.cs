using UnityEngine;
using System.Collections;

public class RespawnCollectables : MonoBehaviour
{

    float respawnTime;

    // Use this for initialization
    void Start()
    {
        respawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(this.gameObject.activeSelf);
        if(!this.gameObject.activeSelf){
            print("RESPAWN");
            respawnTime += Time.deltaTime;
            if(respawnTime > 5){
                respawnTime = 0;
                this.gameObject.SetActive(true);
            }
        }
    }
}
