using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    ActorStats stats;

	// Use this for initialization
	void Start () {
        stats = GetComponent<ActorStats>();

        stats[StatTypes.MOV] = 3;
	}
	
	// Update is called once per frame
	void Update () {
        var movChange = Vector2.zero;

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            movChange = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            movChange = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            movChange = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            movChange = Vector2.down;
        }

        if (movChange != Vector2.zero) {
            var currentPos = transform.position;
            transform.position = new Vector3(currentPos.x + movChange.x, currentPos.y + movChange.y);

            stats[StatTypes.MOV] -= 1;
            print($"MOV: {stats[StatTypes.MOV]}");
        }
    }
}
