using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject point_bubble;
	public Graph graph;

	private Transform boardHolder;

	private float bubble_spacing = 2.0f;

	public void setupBoard(){
		boardHolder = new GameObject("Board").transform;

		// // Create the point bubbles
		// for (int x = 0; x < 10; ++x){
		// 	GameObject instance = Instantiate(point_bubble, new Vector3(bubble_spacing * x, 0, 0), Quaternion.identity) as GameObject;
		// 	instance.transform.SetParent(boardHolder);
		// }

		for (int i = 0; i < graph.test_path.points.Count; ++i){
			Vector2 current_point = graph.test_path.points[i];
			GameObject instance = Instantiate(point_bubble, new Vector3(current_point.x, current_point.y, 0), Quaternion.identity) as GameObject;

			instance.transform.SetParent(boardHolder);
		}
	}

	// Use this for initialization
	void Start () {
		graph = new Graph();

		setupBoard();
	}

	// Update is called once per frame
	void Update () {

	}
}
