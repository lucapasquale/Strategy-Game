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

        bool inMoveRange = RangeManager.MoveRange.Contains(tile);
        bool inAbilityRange = RangeManager.AbilityRangeAndOrigin.ContainsKey(tile);

        // Restart turn if out of range
        if (!inMoveRange && !inAbilityRange) {
            owner.ChangeState<RestartUnitState>();
            return;
        }

        // Just move to empty tile
        if (inMoveRange) {
            actor.turn.SelectMovement(tile);
            owner.ChangeState<PerformMovement>();
            return;
        }

        // If not a target, return
        if (!tile.content || !actor.GetComponentInChildren<AbilityTarget>().IsTarget(tile)) {
            return;
        }
  
        var attackOrigins = RangeManager.AbilityRangeAndOrigin[tile];

        // If already in range, go straight to attack 
        if (attackOrigins.Contains(actor.Tile)) {
            actor.turn.SelectTarget(tile);
            owner.ChangeState<ConfirmTargetState>();
            return;
        }

        // If not in range to target, move to one of origins
        actor.turn.SelectMovement(attackOrigins[0]);
        actor.turn.SelectTarget(tile);
        owner.ChangeState<PerformMovement>();
    }
}