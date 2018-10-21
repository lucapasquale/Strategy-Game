using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityRange : MonoBehaviour
{
    public int range = 1;

    public virtual List<Tile> GetTilesInRange(Board board) {
        Unit unit = GetComponentInParent<Unit>();
        return GetTilesInRange(board, unit.Tile);
    }

    public abstract List<Tile> GetTilesInRange(Board board, Tile startTile);
}