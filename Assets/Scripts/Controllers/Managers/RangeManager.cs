using System.Collections.Generic;
using UnityEngine;

public class RangeManager : Controller
{
    public List<Tile> MoveRange { get; private set; } = new List<Tile>();
    public Dictionary<Tile, List<Tile>> AbilityRangeAndOrigin { get; private set; } = new Dictionary<Tile, List<Tile>>();

    public void GetRanges(Unit unit) {
        MoveRange = GetMovementRange(unit);
        AbilityRangeAndOrigin = GetAbilityRangeAndOriginTiles(unit);
    }


    private List<Tile> GetMovementRange(Unit unit) {
        if (unit.turn.hasUnitMoved) {
            return new List<Tile>() { unit.Tile };
        }

        Movement mover = unit.GetComponentInChildren<Movement>();
        return mover.GetTilesInRange(owner.board);
    }

    private Dictionary<Tile, List<Tile>> GetAbilityRangeAndOriginTiles(Unit unit) {
        var abilityRange = unit.GetComponentInChildren<AbilityRange>();

        var attackOrigins = new Dictionary<Tile, List<Tile>>();
        foreach (var movableTile in MoveRange) {
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