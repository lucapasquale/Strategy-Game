using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    private List<Tile> tilesToMove;

    public override void Enter() {
        base.Enter();

        Unit unit = RoundController.current;
        Movement mover = unit.GetComponent<Movement>();

        // Needs to GetTilesInRange again to save pathfinding on tiles
        tilesToMove = mover.GetTilesInRange(Board);
        SelectActTiles(unit);
        tilesToMove = mover.GetTilesInRange(Board);

        Board.SelectTiles(tilesToMove, Color.green);
    }

    public override void Exit() {
        base.Exit();

        Board.ClearSelection();
        tilesToMove = null;
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);

        if (tilesToMove.Contains(tile)) {
            SelectTile(e.info);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        owner.ChangeState<SelectUnitState>();
    }

    private void SelectActTiles(Unit unit) {
        AbilityRange ar = unit.GetComponentInChildren<AbilityRange>();
        var tilesInActRange = new List<Tile>();

        foreach (var movableTile in tilesToMove) {
            var targetTiles = ar.GetTilesInRange(Board, movableTile);
            targetTiles.RemoveAll(t =>
                t == unit.Tile
                || tilesToMove.Contains(t)
                || tilesInActRange.Contains(t)
            );

            foreach (var target in targetTiles) {
                tilesInActRange.Add(target);
            }
        }

        Board.SelectTiles(tilesInActRange, Color.magenta);

        tilesInActRange.RemoveAll(t => t.content == null);
        Board.SelectTiles(tilesInActRange, Color.red);
    }
}