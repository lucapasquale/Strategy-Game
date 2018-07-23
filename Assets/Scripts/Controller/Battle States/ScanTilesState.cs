using System.Collections;
using System.Collections.Generic;

public class ScanTilesState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine("Sequence");
    }

    private IEnumerator Sequence() {
        Unit unit = RoundController.Current;

        var moveTiles = GetMoveTiles(unit);
        var actOriginTiles = GetActOriginTiles(unit, moveTiles);

        SelectionController.SetTiles(moveTiles, actOriginTiles);
        yield return null;

        owner.ChangeState<SelectActionState>();
    }

    private List<Tile> GetMoveTiles(Unit unit) {
        Movement mover = unit.GetComponent<Movement>();
        return mover.GetTilesInRange(Board);
    }

    private Dictionary<Tile, List<Tile>> GetActOriginTiles(Unit unit, List<Tile> moveTiles) {
        AbilityRange ar = unit.GetComponentInChildren<AbilityRange>();
        var attackOrigins = new Dictionary<Tile, List<Tile>>();

        foreach (var movableTile in moveTiles) {
            var targetTiles = ar.GetTilesInRange(Board, movableTile);

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