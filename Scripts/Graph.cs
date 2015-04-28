using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;


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

	private bool is_horizontal;
	private Node start;
	private Node end;

	private bool is_reversed;

	public GamePath(Node a, Node b, int num_points){
		start = a;
		end = b;

		points = new List<Vector2>();

		// Given node a and b, and a step size, create a list of points representing the path between the two nodes.
		Vector2 p0 = a.position;
		// Vector2 p1 = (b.position - a.position) / 3 + a.position;
		Vector2 p1 = new Vector2(0, 0);
		Vector2 p2 = (a.position - b.position) / 3 + b.position;
		Vector2 p3 = b.position;

		float t = 0;
		while (t < 1){
			Vector2 current_point = thirdOrderBezier(p0, p1, p2, p3, t);
			points.Add(current_point);
			t += step_size;
		}
		// Create point for t=1
		Vector2 last_point = thirdOrderBezier(a.position, a.position, b.position, b.position, 1);
		points.Add(last_point);

		correctPointOrdering();

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

	public Vector2 getPointAtIndex(int index){
		int num_points = getNumPoints();
		if ((index >= 0) && (index < num_points)){
			return points[index];
		} else if (index < 0) {
			return points[0];
		} else if (index >= num_points){
			return points[num_points];
		} else {
			// impossibruuuu
			return new Vector2(0, 0);
		}
	}

	public bool isHorizontal(){
		return is_horizontal;
	}

	public bool isVertical(){
		return !is_horizontal;
	}

	public int getIndexForNode(Node a){
		int start_index, end_index;

		if (is_reversed){
			start_index = getNumPoints() - 1;
			end_index = 0;
		} else {
			start_index = 0;
			end_index = getNumPoints() - 1;
		}

		if (a == start){
			return start_index;
		} else if (a == end) {
			return end_index;
		} else {
			return 0;
		}
	}

	private void correctPointOrdering(){
		// Make this path conform to the standard that incrementing the index will move you to the right, or down. Decrementing the index will move you up or left.

		// Calculate the vector from the first point to the last point
		Vector2 dir_vector = points[getNumPoints() - 1] - points[0];

		Debug.Log(dir_vector);
		if (Math.Abs(dir_vector.x) > Math.Abs(dir_vector.y)){
			// The direction vector points more horizontally.
			is_horizontal = true;

			// If the direction vector is pointing to the left, reverse the list
			if (dir_vector.x < 0){
				is_reversed = true;
				points.Reverse();
			} else {
				is_reversed = false;
			}
		} else {
			Debug.Log("vertical");
			// The direction vector points more vertically.
			is_horizontal = false;

			// If the direction vector is pointing down, reverse the list
			if (dir_vector.y < 0){
				is_reversed = true;
				points.Reverse();
			} else {
				is_reversed = false;
			}

		}


	}

	public bool canMoveTo(int index){
		return (index >= 0) && (index < getNumPoints());
	}

	public int getNumPoints(){
		return points.Count;
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

	public List<Node> nodes = new List<Node>();
	public AdjacencyList adj_list = new AdjacencyList();
	public PathHolder path_holder;

	public Graph(){
		// Create an example graph with adjacency list
		nodes.Add(new Node(new Vector2(-8.0f, 8.0f)));
		nodes.Add(new Node(new Vector2(8.0f, 8.0f)));
		nodes.Add(new Node(new Vector2(-8.0f, -8.0f)));
		nodes.Add(new Node(new Vector2(8.0f, -8.0f)));

		adj_list.addUndirectedEdge(nodes[0], nodes[1]);
		adj_list.addUndirectedEdge(nodes[0], nodes[2]);
		adj_list.addUndirectedEdge(nodes[1], nodes[3]);
		adj_list.addUndirectedEdge(nodes[2], nodes[3]);

		// Create the paths from the adjacency list
		path_holder = new PathHolder(adj_list);

	}

	public List<GamePath> getAllPaths(){
		List<GamePath> all_paths = new List<GamePath>();
		foreach (Node node in nodes){
			foreach(Node adjacent in adj_list.getAdjacentNodes(node)){
				GamePath path = path_holder.getPathBetween(node, adjacent);
				if (!all_paths.Contains(path)){
					all_paths.Add(path);
				}
			}
		}
		return all_paths;
	}

	public GamePath getDefaultPath(){
		return path_holder.getPathBetween(nodes[0], nodes[1]);
	}

	public Node findClosestNodeToPoint(Vector2 point){
		// Not too many nodes so this is k
		foreach (Node node in nodes){
			float dist = (point - node.position).magnitude;
			if (dist < float.Epsilon){
				return node;
			}
		}
		return new Node(new Vector2(0, 0));
	}

	public GamePath getRandomPathFromNode(Node a){
		List<Node> adjacent = adj_list.getAdjacentNodes(a);
		int rand_index = Random.Range(0, adjacent.Count);
		Node b = adjacent[rand_index];
		return path_holder.getPathBetween(a, b);
	}

}
