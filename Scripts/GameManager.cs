using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public BoardManager board_script;

	public GameObject player;

	public Transform unit_holder;

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
		unit_holder = new GameObject("Units").transform;

		board_script = GetComponent<BoardManager>();

		createPlayer();
		createEnemies();
	}

	private void createPlayer(){
		Node player_start = board_script.graph.getPlayerStartNode();
		GameObject player_instance = Instantiate(player, player_start.position, Quaternion.identity) as GameObject;

		player_instance.transform.SetParent(unit_holder);
	}

	private void createEnemies(){

	}

	// Update is called once per frame
	void Update () {

	}
}
