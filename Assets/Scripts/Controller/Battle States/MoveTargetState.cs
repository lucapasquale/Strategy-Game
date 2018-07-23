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
        Unit actor = RoundController.Current;

        // Skip turn
        if (tile == actor.Tile) {
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        // Just move
        if (SelectionController.MoveTiles.Contains(tile)) {
            SelectionController.SelectMove(tile);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        // Undo Selection
        if (!tile.content || !SelectionController.ActOriginTiles.ContainsKey(tile)) {
            owner.ChangeState<SelectUnitState>();
            return;
        }

        // Move to position and act
        Unit target = tile.content.GetComponent<Unit>();
        if (target.alliance == actor.alliance.GetOpposing()) {
            var attackOrigins = SelectionController.ActOriginTiles[tile];

            SelectionController.SelectMove(attackOrigins[0]);
            SelectionController.SelectAct(tile);

            owner.ChangeState<MoveSequenceState>();
        }
    }
}