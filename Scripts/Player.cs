using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		int horizontal = (int) Input.GetAxisRaw("Horizontal");;
		int vertical = (int) Input.GetAxisRaw("Vertical");

		if (horizontal != 0){
			vertical = 0;
		}

		if (horizontal != 0){
			animator.SetBool("moving_vertical", false);
		} else if (vertical != 0){
			animator.SetBool("moving_vertical", true);
		}

	}

}
