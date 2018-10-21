public class AbilityTargetState : BattleState
{
    public override void Enter() {
        base.Enter();

        RangeManager.GetRanges(owner.roundController.Current);
        AreaHighlightManager.Match();
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

        // if select itself, end turn
        Unit actor = RoundController.Current;
        if (target == actor) {
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        // do action on target
        if (target.alliance == actor.alliance.GetOpposing()) {
            SelectionManager.SelectTarget(tile);
            owner.ChangeState<PerformAbilityState>();
        }
    }
}