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
		while (t < 1){
			Vector2 current_point = thirdOrderBezier(a.position, a.position + new Vector2(1, 0), b.position - new Vector2(1, 0), b.position, t);
			points.Add(current_point);
			t += step_size;
		}
		// Create point for t=1
		Vector2 last_point = thirdOrderBezier(a.position, a.position, b.position, b.position, 1);
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
public class AdjacencyList {
	public Dictionary<Node, List<Node>> map = new Dictionary<Node, List<Node>>();

	public AdjacencyList(){

	}

	public void addUndirectedEdge(Node a, Node b){
		if (!map.ContainsKey(a)){
			map.Add(a, new List<Node>());
		}

		if (!map[a].Contains(b)){
			map[a].Add(b);
		}

		if (!map.ContainsKey(b)){
			map.Add(b, new List<Node>());
		}

		if (!map[b].Contains(a)){
			map[b].Add(a);
		}

	}

	public List<Node> getAdjacentNodes(Node a){
		return map[a];
	}
}

[Serializable]
public class PathHolder {
	private Dictionary<Node, Dictionary<Node, GamePath>> path_map = new Dictionary<Node, Dictionary<Node, GamePath>>();

	public PathHolder(AdjacencyList adj_list){
		// Create all of the paths in the adjacency list
		foreach(KeyValuePair<Node, List<Node>> entry in adj_list.map){
			foreach(Node node in entry.Value){
				addUndirectedPath(entry.Key, node);
			}
		}
	}

	public void addUndirectedPath(Node a, Node b){
		GamePath path = new GamePath(a, b, 10);

		// Create the path if the path hasn't been added already
		if (!path_map.ContainsKey(a)){
			path_map.Add(a, new Dictionary<Node, GamePath>());
		}
		if (!path_map[a].ContainsKey(b)){
			path_map[a].Add(b, path);
		}

		if (!path_map.ContainsKey(b)){
			path_map.Add(b, new Dictionary<Node, GamePath>());
		}
		if (!path_map[b].ContainsKey(a)){
			path_map[b].Add(a, path);
		}

	}

	public GamePath getPathBetween(Node a, Node b){
		return path_map[a][b];
	}

}

[Serializable]
public class Graph {

	public AdjacencyList adj_list = new AdjacencyList();
	public PathHolder path_holder;
	public GamePath test_path;

	public Graph(){
		// Create an example graph with adjacency list
		Node a = new Node(new Vector2(-4.0f, 4.0f));
		Node b = new Node(new Vector2(4.0f, 4.0f));
		Node c = new Node(new Vector2(-4.0f, -4.0f));
		Node d = new Node(new Vector2(4.0f, -4.0f));

		adj_list.addUndirectedEdge(a, b);
		adj_list.addUndirectedEdge(a, c);
		adj_list.addUndirectedEdge(b, d);
		adj_list.addUndirectedEdge(c, d);

		// Create the paths from the adjacency list
		path_holder = new PathHolder(adj_list);

		test_path = path_holder.getPathBetween(c, d);

	}

}
