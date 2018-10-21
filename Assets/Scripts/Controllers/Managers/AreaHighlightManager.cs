using UnityEngine;
using System.Collections.Generic;

public class AreaHighlightManager : Controller
{
    public Color moveColor;
    public Color attackColor;
    public Color targetColor;

    private List<Tile> highlights = new List<Tile>();


    public void Match() {
        Unit actor = owner.roundController.Current;
        AbilityTarget abilityTarget = actor.GetComponentInChildren<AbilityTarget>();

        var attackTiles = new List<Tile>();
        var targetTiles = new List<Tile>();

        foreach (Tile actionTile in owner.rangeManager.AbilityRangeAndOrigin.Keys) {
            attackTiles.Add(actionTile);

            if (abilityTarget.IsTarget(actionTile)) {
                targetTiles.Add(actionTile);
            }
        }

        SelectTiles(attackTiles, attackColor);
        SelectTiles(targetTiles, targetColor);

        if (!actor.turn.hasUnitMoved) {
            SelectTiles(owner.rangeManager.MoveRange, moveColor);
        }
    }

    public void Clear() {
        foreach (Tile t in highlights) {
            t.Paint(Color.clear);
        }
    }


    private void SelectTiles(List<Tile> tiles, Color color) {
        for (int i = tiles.Count - 1; i >= 0; --i) {
            tiles[i].Paint(color);
            highlights.Add(tiles[i]);
        }
    }
}