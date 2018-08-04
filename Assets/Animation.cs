using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {

	public Animator animator;

	float inputH;
	float inputV;
	

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		inputH = Input.GetAxis("Horizontal");
		inputV = Input.GetAxis("Vertical");

		animator.SetFloat("inputH", inputH);
		animator.SetFloat("inputV", inputV);
	}
}
