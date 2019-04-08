using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	Animator animator;
	public float speed = 1f;
    bool idle, walking;


	void Start () {
		animator = GetComponent<Animator> ();
        idle = false;
        walking = false;
	}
	
	void Update () {
        
        animator.speed = speed;
        if (Input.GetKeyDown("d"))
        {
            walking = true;
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            animator.SetTrigger("run");
        }
        if (Input.GetKeyUp("d"))
        {
            walking = false;
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            animator.SetTrigger("run");

        }

    }

	public void run() {
		animator.SetTrigger ("run");
	}

	public void jump() {
		animator.SetTrigger ("jump");
	}

	public void attack1() {
		animator.SetTrigger ("attack1");
	}

	public void attack2() {
		animator.SetTrigger ("attack2");
	}

	public void attack3() {
		animator.SetTrigger ("attack3");
	}

	public void skill() {
		animator.SetTrigger ("skill");
	}
}
