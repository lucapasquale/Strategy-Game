using UnityEngine;
using System.Collections;

public class MoveTargetState : BattleState
{
    protected override void OnMove(object sender, object args) {
        var input = (Point)args;
        SelectTile(pos + input);
    }
}