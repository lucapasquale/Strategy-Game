public class SelectActionState : BattleState
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

        bool isMoveTile = RangeManager.MoveRange.Contains(tile);
        bool isAbilityTile = RangeManager.AbilityRangeAndOrigin.ContainsKey(tile);

        // Restart turn if back to start or out of range
        if (tile == actor.turn.startTile || (!isMoveTile && !isAbilityTile)) {
            owner.ChangeState<RestartUnitState>();
            return;
        }

        // Just move to empty tile
        if (isMoveTile) {
            SelectionManager.SelectMovement(tile);
            owner.ChangeState<PerformMovement>();
            return;
        }

        // If not a target, return
        if (!actor.GetComponentInChildren<AbilityTarget>().IsTarget(tile)) {
            return;
        }

        // If in range, attack
        if (RangeManager.AbilityRangeAndOrigin[tile].Contains(actor.Tile)) {
            SelectionManager.SelectTarget(tile);
            owner.ChangeState<PerformAbilityState>();
            return;
        }

        // If not in range to target, move to one of origins
        if (tile.content != null) {
            var attackOrigins = RangeManager.AbilityRangeAndOrigin[tile];
            SelectionManager.SelectMovement(attackOrigins[0]);

            SelectionManager.SelectTarget(tile);
            owner.ChangeState<PerformMovement>();
            return;
        }
    }
}