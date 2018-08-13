using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicturret : MonoBehaviour {

	public GameObject bullet;
	public float bulletForce = 40;
	private Rigidbody rBody;

	// Use this for initialization
	void Start () {

		rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float intialTime = Time.deltaTime;
		if(Time.deltaTime == intialTime+2)
		{
			GameObject current = (GameObject)Instantiate(bullet, this.transform.position, this.transform.rotation);
			current.GetComponent<Rigidbody>().velocity = this.transform.forward * bulletForce;
		}
	}
}
