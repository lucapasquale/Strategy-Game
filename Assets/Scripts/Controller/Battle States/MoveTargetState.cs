using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    public override void Exit() {
        base.Exit();

        Board.ClearSelection();
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);
        Unit actor = RoundController.current;

        if (tile == actor.Tile) {
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        if (SelectionController.MovableTiles.Contains(tile)) {
            SelectionController.SelectMove(tile);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        if (!tile.content || !SelectionController.ActionableTiles.ContainsKey(tile)) {
            owner.ChangeState<SelectUnitState>();
            return;
        }

        Unit target = tile.content.GetComponent<Unit>();
        if (target.alliance == actor.alliance.GetOpposing()) {
            var attackOrigins = SelectionController.ActionableTiles[tile];

            SelectionController.SelectMove(attackOrigins[0]);
            SelectionController.SelectAct(tile);

            owner.ChangeState<MoveSequenceState>();
            return;
        }
    }
}