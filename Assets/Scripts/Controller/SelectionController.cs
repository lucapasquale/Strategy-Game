using UnityEngine;
using System.Collections.Generic;

public class SelectionController : MonoBehaviour
{
    public Color moveColor;
    public Color attackRangeColor;
    public Color attackTargetColor;

    private BattleController owner;

    public void Match() {
        var target = owner.roundController.Current.GetComponentInChildren<AbilityTarget>();

        var emptyActionTiles = new List<Tile>();
        var filledActionTiles = new List<Tile>();

        foreach (var actionTile in owner.rangeController.ActOriginTiles) {
            if (actionTile.Key.content == null || !target.IsTarget(actionTile.Key)) {
                emptyActionTiles.Add(actionTile.Key);
                continue;
            }

            filledActionTiles.Add(actionTile.Key);
        }

        SelectTiles(emptyActionTiles, attackRangeColor);
        SelectTiles(filledActionTiles, attackTargetColor);

        if (!owner.roundController.Current.turn.hasUnitMoved) {
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

    private void Awake() {
        owner = GetComponentInParent<BattleController>();
    }
}