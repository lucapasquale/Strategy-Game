using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTargetState : BattleState
{
    private List<Tile> tilesInRange;

    public override void Enter() {
        base.Enter();
        Movement mover = roundController.current.GetComponent<Movement>();
        tilesInRange = mover.GetTilesInRange(board);
        board.SelectTiles(tilesInRange);
    }

    public override void Exit() {
        base.Exit();
        board.DeSelectTiles(tilesInRange);
        tilesInRange = null;
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = board.GetTile(e.info);

        if (tilesInRange.Contains(tile)) {
            SelectTile(e.info);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        owner.ChangeState<SelectUnitState>();
    }
}