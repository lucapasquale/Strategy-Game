public class AbilityTargetState : BattleState
{
    public override void Enter() {
        base.Enter();
        SelectionController.UpdateSelections();
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

        Unit actor = RoundController.Current;
        if (target == actor) {
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        if (target.alliance == actor.alliance.GetOpposing()) {
            SelectionController.SelectAct(tile);
            owner.ChangeState<PerformAbilityState>();
        }
    }
}