using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    private List<Tile> tilesInRange;

    public override void Enter() {
        base.Enter();
        Movement mover = roundController.current.GetComponent<Movement>();
        tilesInRange = mover.GetTilesInRange(board);
        board.SelectTiles(tilesInRange, Color.green);
    }

    public override void Exit() {
        base.Exit();
        board.SelectTiles(tilesInRange, Color.white);
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