using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	public Tile baseTile;

	const int MAP_SIZE = 5;

	// Use this for initialization
	void Start() {
		for (int x = -MAP_SIZE; x < MAP_SIZE; x++) {
			for (int y = -MAP_SIZE; y < MAP_SIZE; y++) {
				var instance = Instantiate(baseTile, new Vector2(x, y), Quaternion.identity);
				instance.transform.parent = transform;
			}
		}
	}
}
