using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject point_bubble;
	public Graph graph;

	private Transform boardHolder;

	private float bubble_spacing = 2.0f;

	public void setupBoard(){
		boardHolder = new GameObject("Board").transform;



	}

	public void drawPathAsCurve(GamePath path){

		for (int i = 1; i < path.points.Count; ++i){
			Vector2 prev2d = path.points[i - 1];
			Vector2 current2d = path.points[i];

			Vector3 prev = new Vector3(prev2d.x, prev2d.y, 0f);
			Vector3 current = new Vector3(current2d.x, current2d.y, 0f);

			Gizmos.color = Color.white;
			Gizmos.DrawLine(prev, current);

		}
	}

	public void fillPathWithPointBubbles(GamePath path){
		for (int i = 0; i < path.points.Count; ++i){
			// Get the current point as a vector
			Vector2 current_point = path.points[i];

			// Create a point bubble at that position
			GameObject instance = Instantiate(point_bubble, new Vector3(current_point.x, current_point.y, 0), Quaternion.identity) as GameObject;

			instance.transform.SetParent(boardHolder);
		}
	}

	void OnDrawGizmos(){
		foreach (GamePath path in graph.getAllPaths()){
			drawPathAsCurve(path);
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
