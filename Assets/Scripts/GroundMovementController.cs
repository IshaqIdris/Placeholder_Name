using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementController : MonoBehaviour {
    private Vector2 input;

	public Transform camPivot;
	public Transform cam;
	float heading = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		heading += Input.GetAxis("Mouse X")*Time.deltaTime*180;
		camPivot.rotation = Quaternion.Euler(0,heading,0);

		input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = Vector2.ClampMagnitude(input,1);

		Vector3 camF=cam.forward;

		Vector3 camR=cam.right;

		camF.y = 0;
		camR.y = 0;
		camF = camF.normalized;
		camR = camR.normalized;
		//transform.position += new Vector3(input.x,0,input.y)*Time.deltaTime*5;

		transform.transform.position += (camF*input.y + camR*input.x)*Time.deltaTime*5;
	}
}
