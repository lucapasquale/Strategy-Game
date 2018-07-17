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
        Unit selectedUnit = tile.content.GetComponent<Unit>();

        if (selectedUnit) {
            turn.Change(selectedUnit);
            owner.ChangeState<MoveTargetState>();
        }
    }
}