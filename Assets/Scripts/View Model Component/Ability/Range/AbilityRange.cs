using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityRange : MonoBehaviour
{
    public int range = 1;
    public virtual bool DirectionOriented { get { return false; } }

    public abstract List<Tile> GetTilesInRange(Board board, Tile startTile);
}