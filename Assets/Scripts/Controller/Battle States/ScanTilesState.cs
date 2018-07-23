using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanTilesState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine("Sequence");
    }

    private IEnumerator Sequence() {
        Unit unit = RoundController.current;

        SelectMoveTiles(unit);
        SelectActTiles(unit);

        yield return null;

        owner.ChangeState<MoveTargetState>();
    }

    private void SelectMoveTiles(Unit unit) {
        Movement mover = unit.GetComponent<Movement>();
        SelectionController.SetMovable(mover.GetTilesInRange(Board));
    }

    private void SelectActTiles(Unit unit) {
        AbilityRange ar = unit.GetComponentInChildren<AbilityRange>();
        var attackOrigins = new Dictionary<Tile, List<Tile>>();

        foreach (var movableTile in SelectionController.MovableTiles) {
            var targetTiles = ar.GetTilesInRange(Board, movableTile);

            foreach (var target in targetTiles) {
                if (!attackOrigins.ContainsKey(target)) {
                    attackOrigins.Add(target, new List<Tile>());
                }

                attackOrigins[target].Add(movableTile);
            }
        }

        SelectionController.SetActionable(attackOrigins);
    }
}