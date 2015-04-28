using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public BoardManager board_script;

	public GameObject player;

	public Transform player_holder;

	// Use this for initialization
	void Awake () {
		if (instance == null){
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		player_holder = new GameObject("Players").transform;

		board_script = GetComponent<BoardManager>();
		GameObject player_instance = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

		player_instance.transform.SetParent(player_holder);

	}

	// Update is called once per frame
	void Update () {

	}
}
