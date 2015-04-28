using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {

	private int progress = 0;
	private int direction = 1;
	private GamePath current_path;
	private BoxCollider2D box_collider;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		box_collider = GetComponent<BoxCollider2D>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

	}
}
