using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState
{
    protected override void OnMove(object sender, object args) {
        var input = (Point)args;
        SelectTile(pos + input);
    }

    protected override void OnFire(object sender, object args) {
        GameObject content = owner.currentTile.content;

        if (content != null) {
            owner.currentUnit = content.GetComponent<Unit>();
            owner.ChangeState<MoveTargetState>();
        }
    }
}