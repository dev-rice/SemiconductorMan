using UnityEngine;
using System.Collections;
using System;

public abstract class MovingObject : MonoBehaviour {

	public GamePath current_path;
	public Graph graph;

	public float moveTime = 0.2f;

	private int progress = 0;
	protected int direction = 1;
	private int prev_dir;
	private BoxCollider2D box_collider;
	private Rigidbody2D rb2d;

	private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Start () {
		box_collider = GetComponent<BoxCollider2D>();
		rb2d = GetComponent<Rigidbody2D>();

		graph = GameManager.instance.board_script.graph;
		current_path = graph.getDefaultPath();

		Vector2 new_pos_2d = current_path.getPointAtIndex(progress);
		transform.position = new_pos_2d;
		prev_dir = direction;

		inverseMoveTime = 1f / moveTime;
	}

	protected virtual void Update(){
		// If we have changed direction, then update the progress index so we move towards the previous point
		if (prev_dir != direction){
			progress += direction;
		}

		Vector2 next_pos2d = current_path.getPointAtIndex(progress);
		Vector3 next_pos = new Vector3(next_pos2d.x, next_pos2d.y, 0);

		// Update the rigidbody position by moving towards our target from our current position
		Vector3 newPosition = Vector3.MoveTowards(rb2d.position, next_pos, inverseMoveTime * Time.deltaTime);
		rb2d.MovePosition(newPosition);

		// Check how long until we reach the target point
		float sqrRemainingDistance = (transform.position - next_pos).sqrMagnitude;
		// If we have reached it (close enough) then set the progress index to the next one
		bool are_close = (sqrRemainingDistance < float.Epsilon);
		bool can_move_to_next = current_path.canMoveTo(progress + direction);
		if (are_close && can_move_to_next){
			// Update the progress index
			progress += direction;
		} else if (are_close && !can_move_to_next) {
			// Change the current path if we are close to the end and cannot move to the next point in the path

			// Get the closest node to or location
			Node closest = graph.findClosestNodeToPoint(rb2d.position);

			// Find a random path from that node thats not the current path
			GamePath old_path = current_path;
			while(current_path == old_path){
				current_path = graph.getRandomPathFromNode(closest);
			}

		}

		// Keep track of our direction to see if it changes between updates
		prev_dir = direction;

	}

}
