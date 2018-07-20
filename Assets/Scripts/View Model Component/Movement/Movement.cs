using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int range;
    protected Unit unit;
    protected Transform jumper;

    public abstract IEnumerator Traverse(Tile tile);

    public virtual List<Tile> GetTilesInRange(Board board) {
        List<Tile> retValue = board.Search(unit.Tile, ExpandSearch);
        Filter(retValue);
        return retValue;
    }

    protected virtual void Awake() {
        unit = GetComponent<Unit>();
        jumper = transform.Find("Jumper");
    }

    protected virtual bool ExpandSearch(Tile from, Tile to) {
        return (from.distance + 1) <= range;
    }

    protected virtual void Filter(List<Tile> tiles) {
        for (int i = tiles.Count - 1; i >= 0; --i)
            if (tiles[i].content != null)
                tiles.RemoveAt(i);
    }

    protected virtual IEnumerator Turn(Directions dir) {
        TransformLocalEulerTweener t = (TransformLocalEulerTweener)transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);

        // When rotating between North and West, we must make an exception so it looks like the unit
        // rotates the most efficient way (since 0 and 360 are treated the same)
        if (Mathf.Approximately(t.startTweenValue.z, 0f) && Mathf.Approximately(t.endTweenValue.z, 270f)) {
            t.startTweenValue = new Vector3(t.startTweenValue.x, t.startTweenValue.y, 360f);
        }
        else if (Mathf.Approximately(t.startTweenValue.z, 270) && Mathf.Approximately(t.endTweenValue.z, 0)) {
            t.endTweenValue = new Vector3(t.startTweenValue.x, t.startTweenValue.y, 360f);
        }

        unit.dir = dir;

        while (t != null)
            yield return null;
    }
}