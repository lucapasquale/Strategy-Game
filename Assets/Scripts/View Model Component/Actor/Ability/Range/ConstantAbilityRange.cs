using System.Collections.Generic;

public class ConstantAbilityRange : AbilityRange
{
    public override List<Tile> GetTilesInRange(Board board, Tile startTile) {
        var tilesInRange = board.Search(startTile, ExpandSearch);
        tilesInRange.Remove(startTile);

        return tilesInRange;
    }

    private bool ExpandSearch(Tile from, Tile to) {
        return (from.distance + 1) <= range;
    }
}