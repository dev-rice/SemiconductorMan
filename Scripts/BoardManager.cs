using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public GameObject point_bubble;
	public Graph1 graph;

	private Transform boardHolder;

	private float bubble_spacing = 2.0f;

	public void setupBoard(){
		boardHolder = new GameObject("Board").transform;

		List<Vector2> all_points = getAllPointLocations();
		putPointBubblesOnPoints(all_points);
	}

	private List<Vector2> getAllPointLocations(){
		List<Vector2> all_points = new List<Vector2>();
		foreach (GamePath path in graph.getAllPaths()){
			List<Vector2> even_points = path.getEvenlySpacedPoints(bubble_spacing);
			foreach(Vector2 point in even_points){
				all_points.Add(point);
			}
		}
		return all_points;
	}

	private void putPointBubblesOnPoints(List<Vector2> points){
		foreach (Vector2 point in points){
			// Create a point bubble at that position
			GameObject instance = Instantiate(point_bubble, new Vector3(point.x, point.y, 0), Quaternion.identity) as GameObject;

			instance.transform.SetParent(boardHolder);
		}
	}

	void OnDrawGizmos(){
		// foreach (GamePath path in graph.getAllPaths()){
		// 	drawPathAsCurve(path);
		// }
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

	// Use this for initialization
	void Start () {
		graph = new Graph1();

		setupBoard();
	}

	// Update is called once per frame
	void Update () {

	}
}
