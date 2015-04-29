using UnityEngine;
using System.Collections;

public class Player : MovingObject {

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
 			} else {
				wants_to_change_axis = true;
				requested_move_dir = (horizontal < 0) ? -1 : 1;
			}
		} else if (vertical != 0){
			if (moving_axis == "vertical"){
				direction = (vertical < 0) ? -1 : 1;
				wants_to_change_axis = false;
				requested_move_dir = direction;
			} else {
				wants_to_change_axis = true;
				requested_move_dir = (vertical < 0) ? -1 : 1;

			}

		}

		base.Update();

	}

}
