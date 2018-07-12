using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    [SerializeField] private GameObject tilePrefab;

    public void Load(LevelData data) {
        for (int i = 0; i < data.tiles.Count; ++i) {
            GameObject instance = Instantiate(tilePrefab, transform) as GameObject;
            Tile t = instance.GetComponent<Tile>();
            t.Load(data.tiles[i]);
            tiles.Add(t.pos, t);
        }
    }
}