using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstantAbilityRange : AbilityRange
{
    public override List<Tile> GetTilesInRange(Board board) {
        var tilesInRange = board.Search(unit.tile, ExpandSearch);
        tilesInRange.Remove(unit.tile);

        return tilesInRange;
    }

    private bool ExpandSearch(Tile from, Tile to) {
        return (from.distance + 1) <= range;
    }
}