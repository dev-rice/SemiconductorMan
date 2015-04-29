using UnityEngine;
using System.Collections;

public class Enemy : MovingObject {

	private Transform target;

	// Use this for initialization
	protected override void Start () {
		base.Start();

		target = GameObject.FindGameObjectWithTag("Player").transform;

		Node start_node = graph.getRandomNode();
		GamePath start_path = graph.getRandomPathFromNode(start_node);
		setCurrentPath(start_path);
	}

	// Update is called once per frame
	protected override void Update () {
		// if (moving_axis == "horizontal"){
		// 	// If the enemy is to the left of the player
		// 	if (target.position.x < transform.position.x){
		// 		direction = -1;
		// 	} else {
		// 		direction = 1;
		// 	}
		// } else if (moving_axis == "vertical"){
		// 	// If the enemy is below the player
		// 	if (target.position.y < transform.position.y){
		// 		direction = -1;
		// 	} else {
		// 		direction = 1;
		// 	}
		// }

		base.Update();
	}

	protected override GamePath findNewPath(Node closest){
		return graph.getRandomPathFromNode(closest);
	}

}
