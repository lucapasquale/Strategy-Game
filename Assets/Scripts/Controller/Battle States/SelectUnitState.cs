using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState
{
    public override void Enter() {
        base.Enter();
        ClearSelection();
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = board.GetTile(e.info);
        if (tile.content == null) {
            return;
        }

        SelectTile(e.info);
        Unit unit = tile.content.GetComponent<Unit>();

        if (unit) {
            roundController.Select(unit);
            owner.ChangeState<MoveTargetState>();
        }
    }
}