using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Node {
	public Vector2 position;

	public Node(Vector2 pos){
		position = pos;
	}
}

[Serializable]
public class GamePath {
	public List<Vector2> points;
	float step_size = 0.1f;

	public GamePath(Node a, Node b, int num_points){
		points = new List<Vector2>();

		// Given node a and b, and a step size, create a list of points representing the path between the two nodes.
		float t = 0;

		Debug.Log("Creating path between " + a.position + " and " + b.position);

		while (t < 1){
			Vector2 current_point = thirdOrderBezier(a.position, a.position + new Vector2(1, 0), b.position - new Vector2(1, 0), b.position, t);
			points.Add(current_point);
			t += step_size;
		}
		// Create point for t=1
		Vector2 last_point = thirdOrderBezier(a.position, a.position + new Vector2(1, 0), b.position - new Vector2(1, 0), b.position, 1);
		points.Add(last_point);

	}

	private Vector2 thirdOrderBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t){
		// p0 and p3 are endpoints
		// p1 and p2 tune the curviness

		float a0 = (float)Math.Pow((1 - t), 3);
		float a1 = (float)Math.Pow((1 - t), 2) * 3 * t;
		float a2 = (1 - t) * 3 * (float)Math.Pow(t, 2);
		float a3 = (float)Math.Pow(t, 3);


		Vector2 point = (a0 * p0) + (a1 * p1) + (a2 * p2) + (a3 * p3);

		return point;
	}
}

[Serializable]
public class Graph {
	private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
	private Dictionary<Node, List<Node>> adj_list = new Dictionary<Node, List<Node>>();

	public GamePath test_path;

	public Graph(){
		// Create an example graph
		Node a = new Node(new Vector2(-4.0f, -2.0f));
		Node b = new Node(new Vector2(-2.0f, -2.0f));
		Node c = new Node(new Vector2(2.0f, 2.0f));
		Node d = new Node(new Vector2(4.0f, 2.0f));

		test_path = new GamePath(b, c, 10);

	}

}
