public class SelectActionState : BattleState
{
    public override void Enter() {
        base.Enter();

        RangeController.UpdateSelections();
        owner.roundController.areaHighlightManager.Match();
    }

    public override void Exit() {
        base.Exit();

        owner.roundController.areaHighlightManager.Clear();
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);
        Unit actor = RoundController.Current;

        // Skip turn
        if (tile == actor.Tile) {
            actor.turn.hasUnitMoved = true;
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        // Just move
        if (RangeController.MoveTiles.Contains(tile)) {
            RangeController.SelectMove(tile);
            owner.ChangeState<MoveSequenceState>();
            return;
        }

        // Undo Selection
        if (!tile.content || !RangeController.ActOriginTiles.ContainsKey(tile)) {
            owner.ChangeState<SelectUnitState>();
            return;
        }

        // Move to position and act
        Unit target = tile.content.GetComponent<Unit>();
        if (target.alliance == actor.alliance.GetOpposing()) {
            var attackOrigins = RangeController.ActOriginTiles[tile];

            RangeController.SelectMove(attackOrigins[0]);
            RangeController.SelectAct(tile);

            owner.ChangeState<MoveSequenceState>();
        }
    }
}