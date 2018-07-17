using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbilityRange : MonoBehaviour
{
    public int range = 1;
    public virtual bool directionOriented { get { return false; } }
    protected Unit unit { get { return GetComponentInParent<Unit>(); } }

    public abstract List<Tile> GetTilesInRange(Board board);
}