using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    private List<Tile> tilesToMove;
    private List<Tile> tilesToAct;

    public override void Enter() {
        base.Enter();

        Unit unit = roundController.current;
        Movement mover = unit.GetComponent<Movement>();

        // Needs to GetTilesInRange again to save pathfinding on tiles
        tilesToMove = mover.GetTilesInRange(board);
        tilesToAct = GetTilesInActRange(unit);
        tilesToMove = mover.GetTilesInRange(board);

        board.SelectTiles(tilesToMove, Color.green);
        board.SelectTiles(tilesToAct, Color.red);
    }

    public override void Exit() {
        base.Exit();
        board.ClearSelection();

        tilesToMove = null;
        tilesToAct = null;
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = board.GetTile(e.info);
        Unit actor = roundController.current;

        if (tile == actor.tile) {
            roundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        if (tilesToMove.Contains(tile)) {
            SelectTile(e.info);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        if (!tile.content || !tilesToAct.Contains(tile)) {
            owner.ChangeState<SelectUnitState>();
            return;
        }

        Unit target = tile.content.GetComponent<Unit>();
        if (target.alliance == actor.alliance.GetOpposing()) {
            SelectTile(e.info);
            print($"Attacking {target}");

            var ability = roundController.current.GetComponentInChildren<Ability>();
            ability.Perform(new List<Tile>() { tile });

            roundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
        }
    }

    private List<Tile> GetTilesInActRange(Unit unit) {
        AbilityRange ar = unit.GetComponentInChildren<AbilityRange>();
        var tilesInActRange = new List<Tile>();

        foreach (var movableTile in tilesToMove) {
            var targetTiles = ar.GetTilesInRange(board, movableTile);
            targetTiles.RemoveAll(t => tilesToMove.Contains(t) || tilesInActRange.Contains(t));

            foreach (var target in targetTiles) {
                tilesInActRange.Add(target);
            }
        }

        tilesInActRange.Remove(unit.tile);
        return tilesInActRange;
    }
}