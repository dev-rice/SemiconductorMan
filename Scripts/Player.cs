using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
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
