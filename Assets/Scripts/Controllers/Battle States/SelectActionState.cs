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

        // Undo selection if back to start or out of range
        if (tile == actor.turn.startTile || (!inMoveRange && !inAbilityRange)) {
            SelectionManager.SelectMovement(actor.turn.startTile);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        // Just move to empty tile
        if (inMoveRange) {
            SelectionManager.SelectMovement(tile);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        SelectionManager.SelectTarget(tile);

        // If not in range, move to one of origins
        if (!RangeManager.AbilityRangeAndOrigin[tile].Contains(actor.Tile)) {
            var attackOrigins = RangeManager.AbilityRangeAndOrigin[tile];
            SelectionManager.SelectMovement(attackOrigins[0]);

            owner.ChangeState<MoveSequenceState>();
            return;
        }


        owner.ChangeState<PerformAbilityState>();
    }
}