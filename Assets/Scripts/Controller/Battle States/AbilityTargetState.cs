using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetState : BattleState
{
    private List<Tile> targetTiles;
    private AbilityRange ar;

    public override void Enter() {
        base.Enter();

        var unit = RoundController.current;

        ar = unit.GetComponentInChildren<AbilityRange>();
        targetTiles = ar.GetTilesInRange(Board, unit.Tile);

        Board.SelectTiles(targetTiles, Color.red);
    }

    public override void Exit() {
        base.Exit();
        Board.SelectTiles(targetTiles, Color.white);
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);
        if (tile.content == null) {
            return;
        }

        Unit target = tile.content.GetComponent<Unit>();
        if (!target) {
            return;
        }

        Unit actor = RoundController.current;
        if (target == actor) {
            EndMove();
            return;
        }

        bool isEnemy = target.alliance == actor.alliance.GetOpposing();
        if (targetTiles.Contains(tile) && isEnemy) {
            //SelectTile(e.info);

            print($"Attacking {target}");
            var ability = RoundController.current.GetComponentInChildren<Ability>();
            ability.Perform(new List<Tile>() { tile });

            EndMove();
        }
    }

    private void EndMove() {
        RoundController.EndTurn();
        owner.ChangeState<SelectUnitState>();
    }
}