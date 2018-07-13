using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTargetState : BattleState
{
    private List<Tile> tiles;

    public override void Enter() {
        base.Enter();
        Movement mover = owner.currentUnit.GetComponent<Movement>();
        tiles = mover.GetTilesInRange(board);
        board.SelectTiles(tiles);
    }

    public override void Exit() {
        base.Exit();
        board.DeSelectTiles(tiles);
        tiles = null;
    }

    protected override void OnMove(object sender, object args) {
        var input = (Point)args;
        SelectTile(pos + input);
    }

    protected override void OnFire(object sender, object args) {
        if (tiles.Contains(owner.currentTile))
            owner.ChangeState<MoveSequenceState>();
    }
}