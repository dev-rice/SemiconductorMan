using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	protected override void Start(){
		base.Start();

		Node start_node = graph.getPlayerStartNode();
		GamePath start_path = graph.getPlayerStartPath();
		setCurrentPath(start_path);
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

	protected override GamePath findNewPath(Node closest){
		GamePath new_path = current_path;
		// Try to change axis
		if (wants_to_change_axis){
			new_path = graph.getPathWithAxisChangeAndDirection(current_path, closest, requested_move_dir);
			// If there is no such path, just keep the same axis
			if (new_path == current_path){
				new_path = graph.getNoAxisChangePathFromNode(current_path, closest);
			}
		} else {
			new_path = graph.getNoAxisChangePathFromNode(current_path, closest);
		}
		return new_path;
	}

}
