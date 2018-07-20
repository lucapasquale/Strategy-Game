using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    private List<Tile> tilesToMove;
    private List<Tile> tilesToAct;

    public override void Enter() {
        base.Enter();

        Unit unit = RoundController.current;
        Movement mover = unit.GetComponent<Movement>();

        // Needs to GetTilesInRange again to save pathfinding on tiles
        tilesToMove = mover.GetTilesInRange(Board);
        tilesToAct = GetTilesInActRange(unit);
        tilesToMove = mover.GetTilesInRange(Board);

        Board.SelectTiles(tilesToMove, Color.green);
        Board.SelectTiles(tilesToAct, Color.red);
    }

    public override void Exit() {
        base.Exit();
        Board.ClearSelection();

        tilesToMove = null;
        tilesToAct = null;
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);
        Unit actor = RoundController.current;

        if (tile == actor.Tile) {
            RoundController.EndTurn();
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

            var ability = RoundController.current.GetComponentInChildren<Ability>();
            ability.Perform(new List<Tile>() { tile });

            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
        }
    }

    private List<Tile> GetTilesInActRange(Unit unit) {
        AbilityRange ar = unit.GetComponentInChildren<AbilityRange>();
        var tilesInActRange = new List<Tile>();

        foreach (var movableTile in tilesToMove) {
            var targetTiles = ar.GetTilesInRange(Board, movableTile);
            targetTiles.RemoveAll(t => tilesToMove.Contains(t) || tilesInActRange.Contains(t));

            foreach (var target in targetTiles) {
                tilesInActRange.Add(target);
            }
        }

        tilesInActRange.Remove(unit.Tile);
        return tilesInActRange;
    }
}