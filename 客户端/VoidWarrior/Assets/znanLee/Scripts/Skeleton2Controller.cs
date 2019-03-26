using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton2Controller : MonoBehaviour {

    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetBool("ToIdle", true);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetBool("ToDie", true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetBool("ToWalk", true);
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            animator.SetBool("ToAttack", true);
        }
    }
}
