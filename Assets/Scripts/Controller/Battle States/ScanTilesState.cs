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
        var actionableTiles = new List<Tile>();

        foreach (var movableTile in SelectionController.MovableTiles) {
            var targetTiles = ar.GetTilesInRange(Board, movableTile);

            targetTiles.RemoveAll(t =>
                t == unit.Tile
                || SelectionController.MovableTiles.Contains(t)
                || actionableTiles.Contains(t)
            );

            foreach (var target in targetTiles) {
                actionableTiles.Add(target);
            }
        }

        SelectionController.SetActionable(actionableTiles);
    }
}