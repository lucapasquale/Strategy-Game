public class SelectActionState : BattleState
{
    public override void Enter() {
        base.Enter();
        AreaHighlightManager.Match();
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);
        Unit actor = RoundController.Current;

        // Skip turn
        if (tile == actor.Tile) {
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        // Undo selection if out of range
        if (!RangeManager.MoveRange.Contains(tile) && !RangeManager.AbilityRangeAndOrigin.ContainsKey(tile)) {
            owner.ChangeState<SelectUnitState>();
            return;
        }

        // Just move
        if (RangeManager.MoveRange.Contains(tile)) {
            SelectionManager.SelectMovement(tile);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        // Move to position and act
        Unit target = tile.content.GetComponent<Unit>();
        if (target.alliance == actor.alliance.GetOpposing()) {
            var attackOrigins = RangeManager.AbilityRangeAndOrigin[tile];

            SelectionManager.SelectMovement(attackOrigins[0]);
            SelectionManager.SelectTarget(tile);

            owner.ChangeState<MoveSequenceState>();
        }
    }
}