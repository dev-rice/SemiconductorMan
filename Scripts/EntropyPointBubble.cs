using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class EntropyPointBubble : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		int randnum = Random.Range(0, 2);
		bool is_reversed = (randnum == 1);
		animator.SetBool("reversed", is_reversed);

	}

	// Update is called once per frame
	void Update () {

	}
}
