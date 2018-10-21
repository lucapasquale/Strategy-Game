public class ConfirmTargetState : BattleState
{
    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);
        Unit actor = RoundController.Current;

        // End turn if select itself
        if (tile == actor.Tile) {
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        // Restart turn if selected is start or empty
        if (tile == actor.turn.startTile || tile.content == null) {
            owner.ChangeState<RestartUnitState>();
            return;
        }

        // If not a target, return
        if (actor.GetComponentInChildren<AbilityTarget>().IsTarget(tile)) {
            return;
        }

        owner.ChangeState<PerformAbilityState>();
    }
}