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
	protected override void Update () {
		int horizontal = (int) Input.GetAxisRaw("Horizontal");;
		int vertical = (int) Input.GetAxisRaw("Vertical");

		if (horizontal != 0){
			vertical = 0;
		}

		if (horizontal != 0){
			if (moving_axis == "horizontal"){
				direction = (horizontal < 0) ? -1 : 1;
				wants_to_change_axis = false;
				requested_move_dir = direction;

				if (direction == -1){
					setBackwardsAnimation();
				} else {
					setForwardsAnimation();
				}
 			} else {
				wants_to_change_axis = true;
				requested_move_dir = (horizontal < 0) ? -1 : 1;
			}
		} else if (vertical != 0){
			if (moving_axis == "vertical"){
				direction = (vertical < 0) ? -1 : 1;
				wants_to_change_axis = false;
				requested_move_dir = direction;

				if (direction == 1){
					setBackwardsAnimation();
				} else {
					setForwardsAnimation();
				}
			} else {
				wants_to_change_axis = true;
				requested_move_dir = (vertical < 0) ? -1 : 1;

			}

		}

		if (moving_axis == "horizontal"){
			setHorizontalAnimation();
		} else if (moving_axis == "vertical"){
			setVerticalAnimation();
		}

		base.Update();

	}

	private void setHorizontalAnimation(){
		animator.SetBool("moving_vertical", false);
	}

	private void setVerticalAnimation(){
		animator.SetBool("moving_vertical", true);
	}

	private void setBackwardsAnimation(){
		animator.SetBool("backwards", true);
	}

	private void setForwardsAnimation(){
		animator.SetBool("backwards", false);
	}

	protected override void setCurrentPath(GamePath path){
		base.setCurrentPath(path);

		if (moving_axis == "horizontal"){
			setHorizontalAnimation();
		} else if (moving_axis == "vertical"){
			setVerticalAnimation();
		}

	}


}
