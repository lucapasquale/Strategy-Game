using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    public override void Exit() {
        base.Exit();

        SelectionController.Clear();
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);

        if (SelectionController.MovableTiles.Contains(tile)) {
            SelectTile(e.info);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        owner.ChangeState<SelectUnitState>();
    }
}