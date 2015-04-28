using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	private Animator animator;

	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator>();

		base.Start();
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
			direction = (horizontal < 0) ? -1 : 1;
		} else if (vertical != 0){
			animator.SetBool("moving_vertical", true);
			direction = (vertical < 0) ? -1 : 1;

		}

		base.Update();

	}

}
