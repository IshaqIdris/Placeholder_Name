using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {

	public Animator animator;

	float inputH;
	float inputV;
	bool jump;
    bool isDead;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		jump =false;
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		inputH = Input.GetAxis("Horizontal");
		inputV = Input.GetAxis("Vertical");

		animator.SetFloat("inputH", inputH);
		animator.SetFloat("inputV", inputV);

		if(Input.GetButton("Jump")){
			jump = true;
		}else{
			jump = false;
		}

        if(isDead){
            animator.SetBool("isDead", isDead);
        }else{
            animator.SetBool("isDead", false);
        }

		animator.SetBool("jump", jump);
	}

    public void setDead(bool dead){
        this.isDead = dead;
    }
}
