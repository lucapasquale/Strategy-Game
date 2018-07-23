using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    [SerializeField] private GameObject tilePrefab;

    private Point[] dirs = new Point[4] {
      new Point(0, 1),
      new Point(0, -1),
      new Point(1, 0),
      new Point(-1, 0)
    };

    public void Load(LevelData data) {
        for (int i = 0; i < data.tiles.Count; ++i) {
            GameObject instance = Instantiate(tilePrefab, transform) as GameObject;
            Tile t = instance.GetComponent<Tile>();
            t.Load(data.tiles[i]);
            tiles.Add(t.pos, t);
        }
    }

    public Tile GetTile(Point p) {
        return tiles.ContainsKey(p) ? tiles[p] : null;
    }

    public void SelectTiles(List<Tile> tiles, Color color) {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<SpriteRenderer>().color = color;
    }

    public void ClearSelection() {
        foreach (Tile t in tiles.Values) {
            t.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public List<Tile> Search(Tile start, Func<Tile, Tile, bool> shouldAddTile) {
        List<Tile> retValue = new List<Tile> { start };

        ClearSearch();
        Queue<Tile> checkNext = new Queue<Tile>();
        Queue<Tile> checkNow = new Queue<Tile>();

        start.distance = 0;
        checkNow.Enqueue(start);

        while (checkNow.Count > 0) {
            Tile t = checkNow.Dequeue();

            for (int i = 0; i < 4; ++i) {
                Tile next = GetTile(t.pos + dirs[i]);

                if (next == null || next.distance <= t.distance + 1)
                    continue;

                if (shouldAddTile(t, next)) {
                    next.distance = t.distance + 1;
                    next.prev = t;
                    checkNext.Enqueue(next);
                    retValue.Add(next);
                }
            }

            if (checkNow.Count == 0)
                SwapReference(ref checkNow, ref checkNext);
        }

        return retValue;
    }

    private void ClearSearch() {
        foreach (Tile t in tiles.Values) {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

    private void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b) {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }
}