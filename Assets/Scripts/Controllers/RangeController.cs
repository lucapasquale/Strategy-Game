using System.Collections.Generic;
using UnityEngine;

public class RangeController : MonoBehaviour
{
    public Tile moveTile;
    public Tile actTile;

    private BattleController owner;

    public List<Tile> MoveTiles { get; private set; } = new List<Tile>();
    public Dictionary<Tile, List<Tile>> ActOriginTiles { get; private set; } = new Dictionary<Tile, List<Tile>>();

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
        Unit unit = owner.roundController.Current;
        if (unit == null) {
            Clear();
            return;
        }

        MoveTiles = GetMoveTiles(unit);
        ActOriginTiles = GetActOriginTiles(unit);
    }

    private void Clear() {
        MoveTiles.Clear();
        moveTile = null;

        ActOriginTiles.Clear();
        actTile = null;
    }

    private void Awake() {
        owner = GetComponentInParent<BattleController>();
    }

    private List<Tile> GetMoveTiles(Unit unit) {
        if (unit.turn.hasUnitMoved) {
            return new List<Tile>() { unit.Tile };
        }

        Movement mover = unit.GetComponent<Movement>();
        return mover.GetTilesInRange(owner.board);
    }

    private Dictionary<Tile, List<Tile>> GetActOriginTiles(Unit unit) {
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