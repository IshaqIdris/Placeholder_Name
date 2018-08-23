using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	// Use this for initialization
	public Transform player;	
	float heading = 0;
	float tilt = 15;
    float camDist = 10;
    float playerHeight = 1;

    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		heading += Input.GetAxis("Mouse X")*Time.deltaTime*1040;
		tilt += Input.GetAxis("Mouse Y")*Time.deltaTime*540;
		tilt = Mathf.Clamp(tilt, -80, 80);//Stop from going upside down
		transform.rotation = Quaternion.Euler(tilt, heading, 0);

		transform.position = player.position - transform.forward * camDist + Vector3.up * playerHeight;
	}
}
