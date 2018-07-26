using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public Tile moveTile;
    public Tile actTile;

    private BattleController owner;

    public List<Tile> MoveTiles { get; private set; }
    public Dictionary<Tile, List<Tile>> ActOriginTiles { get; private set; }

    public void SelectMove(Tile tile) {
        if (!MoveTiles.Contains(tile)) {
            Debug.LogError("Move tile not found");
            return;
        }

        moveTile = tile;
    }

    public void SelectAct(Tile tile) {
        if (!ActOriginTiles.ContainsKey(tile)) {
            Debug.LogError("Act tile not found");
            return;
        }

        actTile = tile;
    }

    public void UpdateSelections() {
        MoveTiles = GetMoveTiles();
        ActOriginTiles = GetActOriginTiles();
        Match();
    }

    public void Clear() {
        MoveTiles = new List<Tile>();
        moveTile = null;

        ActOriginTiles = new Dictionary<Tile, List<Tile>>();
        actTile = null;

        owner.board.ClearSelection();
    }

    private void Match() {
        var emptyActionTiles = new List<Tile>();
        var filledActionTiles = new List<Tile>();

        foreach (var actionTile in ActOriginTiles) {
            if (actionTile.Key.content == null) {
                emptyActionTiles.Add(actionTile.Key);
                continue;
            }

            filledActionTiles.Add(actionTile.Key);
        }

        owner.board.SelectTiles(emptyActionTiles, Color.magenta);
        owner.board.SelectTiles(filledActionTiles, Color.red);

        if (!owner.roundController.Current.turn.hasUnitMoved) {
            owner.board.SelectTiles(MoveTiles, Color.blue);
        }
    }

    private void Awake() {
        owner = GetComponentInParent<BattleController>();
    }

    private List<Tile> GetMoveTiles() {
        Unit unit = owner.roundController.Current;

        if (unit.turn.hasUnitMoved) {
            return new List<Tile>() { unit.Tile };
        }

        Movement mover = unit.GetComponent<Movement>();
        return mover.GetTilesInRange(owner.board);
    }

    private Dictionary<Tile, List<Tile>> GetActOriginTiles() {
        Unit unit = owner.roundController.Current;
        var abilityRange = unit.GetComponentInChildren<AbilityRange>();

        var attackOrigins = new Dictionary<Tile, List<Tile>>();
        foreach (var movableTile in MoveTiles) {
            var targetTiles = abilityRange.GetTilesInRange(owner.board, movableTile);

            foreach (var target in targetTiles) {
                if (!attackOrigins.ContainsKey(target)) {
                    attackOrigins.Add(target, new List<Tile>());
                }

                attackOrigins[target].Add(movableTile);
            }
        }

        return attackOrigins;
    }
}