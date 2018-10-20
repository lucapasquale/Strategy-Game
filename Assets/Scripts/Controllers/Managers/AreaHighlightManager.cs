using UnityEngine;
using System.Collections.Generic;

public class AreaHighlightManager : Controller
{
    public Color moveColor;
    public Color attackColor;
    public Color targetColor;


    public void Match() {
        Unit actor = owner.roundController.Current;
        AbilityTarget abilityTarget = actor.GetComponentInChildren<AbilityTarget>();

        var attackTiles = new List<Tile>();
        var targetTiles = new List<Tile>();

        foreach (Tile actionTile in owner.rangeController.ActOriginTiles.Keys) {
            attackTiles.Add(actionTile);

            if (abilityTarget.IsTarget(actionTile)) {
                targetTiles.Add(actionTile);
            }
        }

        SelectTiles(attackTiles, attackColor);
        SelectTiles(targetTiles, targetColor);

        if (!actor.turn.hasUnitMoved) {
            SelectTiles(owner.rangeController.MoveTiles, moveColor);
        }
    }

    public void Clear() {
        var allTiles = owner.board.tiles;

        foreach (Tile t in allTiles.Values) {
            t.Paint(Color.clear);
        }
    }


    private void SelectTiles(List<Tile> tiles, Color color) {
        for (int i = tiles.Count - 1; i >= 0; --i) {
            tiles[i].Paint(color);
        }
    }
}