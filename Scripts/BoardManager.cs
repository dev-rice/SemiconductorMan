using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject point_bubble;

	private Transform boardHolder;

	private float bubble_spacing = 2.0f;

	public void setupBoard(){
		boardHolder = new GameObject("Board").transform;

		// Create the point bubbles
		for (int x = 0; x < 10; ++x){
			GameObject instance = Instantiate(point_bubble, new Vector3(bubble_spacing * x, 0, 0), Quaternion.identity) as GameObject;
			instance.transform.SetParent(boardHolder);
		}
	}

	// Use this for initialization
	void Start () {
		setupBoard();
	}

	// Update is called once per frame
	void Update () {

	}
}
