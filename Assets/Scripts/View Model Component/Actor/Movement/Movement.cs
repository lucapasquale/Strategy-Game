using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int range;
    protected Unit unit;

    public abstract IEnumerator Traverse(Tile tile, float duration = 0.2f);

    public virtual List<Tile> GetTilesInRange(Board board) {
        List<Tile> retValue = board.Search(unit.Tile, ExpandSearch);
        Filter(retValue);
        return retValue;
    }

    protected virtual void Awake() {
        unit = GetComponentInParent<Unit>();
    }

    protected virtual bool ExpandSearch(Tile from, Tile to) {
        return (from.distance + 1) <= range;
    }

    protected virtual void Filter(List<Tile> tiles) {
        for (int i = tiles.Count - 1; i >= 0; --i)
            if (tiles[i].content != null && tiles[i].content != unit.gameObject)
                tiles.RemoveAt(i);
    }
}