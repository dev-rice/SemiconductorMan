using UnityEngine;
using System.Collections;
using System;

public abstract class MovingObject : MonoBehaviour {

	public GamePath current_path;

	public float moveTime = 0.2f;

	private int progress = 0;
	protected int direction = 1;
	private BoxCollider2D box_collider;
	private Rigidbody2D rb2d;

	private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Start () {
		box_collider = GetComponent<BoxCollider2D>();
		rb2d = GetComponent<Rigidbody2D>();

		current_path = GameManager.instance.board_script.graph.getDefaultPath();

		Vector2 new_pos_2d = current_path.getPointAtIndex(progress);
		transform.position = new_pos_2d;

		inverseMoveTime = 1f / moveTime;
	}

	protected virtual void Update(){
		Vector2 next_pos2d = current_path.getPointAtIndex(progress);
		Vector3 next_pos = new Vector3(next_pos2d.x, next_pos2d.y, 0);

		Vector3 newPosition = Vector3.MoveTowards(rb2d.position, next_pos, inverseMoveTime * Time.deltaTime);
		rb2d.MovePosition(newPosition);
		float sqrRemainingDistance = (transform.position - next_pos).sqrMagnitude;
		if ((sqrRemainingDistance < float.Epsilon) && current_path.canMoveTo(progress + direction)){
			progress += direction;
		}

	}

}
