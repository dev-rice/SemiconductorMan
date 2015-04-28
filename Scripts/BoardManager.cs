using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject point_bubble;
	public Graph graph;

	private Transform boardHolder;

	private float bubble_spacing = 2.0f;

	public void setupBoard(){
		boardHolder = new GameObject("Board").transform;

		foreach (GamePath path in graph.getAllPaths()){
			fillPathWithPointBubbles(path);
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

	// Use this for initialization
	void Start () {
		graph = new Graph();

		setupBoard();
	}

	// Update is called once per frame
	void Update () {

	}
}
