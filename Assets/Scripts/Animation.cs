using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {

	public Animator animator;

	float inputH;
	float inputV;
	bool jump;
    bool isDead;
    float downTime;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		jump =false;
        isDead = false;
        downTime = 0;
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

        //if(downTime >1){
        //    isDead = false;
        //    downTime = 0;
        //}

        if(isDead ){
            //print("DEAD");
            animator.SetBool("isDead", isDead);
            downTime += Time.deltaTime;
            if(downTime > 1){
                isDead = false;
                downTime = 0;
            }

        }else{
            animator.SetBool("isDead", isDead);
        }

		animator.SetBool("jump", jump);
	}

    public void setDead(bool dead){
        this.isDead = dead;
    }
}
