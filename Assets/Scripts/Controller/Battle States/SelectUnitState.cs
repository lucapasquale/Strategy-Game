public class SelectUnitState : BattleState
{
    public override void Enter() {
        base.Enter();

        RoundController.Select(null);
        RangeController.UpdateSelections();
        SelectionController.Clear();
    }

    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);
        if (tile.content == null) {
            RoundController.Select(null);
            return;
        }

        Unit unit = tile.content.GetComponent<Unit>();
        if (unit && unit.turn.IsAvailable() && RoundController.actingSide == unit.alliance) {
            RoundController.Select(unit);
            owner.ChangeState<SelectActionState>();
        }
    }
}