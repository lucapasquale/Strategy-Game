using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour {
	public Point pos;
	public GameObject content;

	void Match() {
		transform.localPosition = new Vector3(pos.x, pos.y);
		transform.localScale = Vector3.one;
	}
}
