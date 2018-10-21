using System.Collections;

public class MoveSequenceState : BattleState
{
    public const string UnitMovedNotification = "MoveSequenceState.UnitMovedNotification";


    public override void Enter() {
        base.Enter();
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence() {
        Unit unit = RoundController.Current;

        Movement mov = unit.GetComponent<Movement>();
        mov.GetTilesInRange(Board);

        Tile destination = SelectionManager.MovementTile;
        yield return StartCoroutine(mov.Traverse(destination));

        unit.turn.hasUnitMoved = true;
        this.PostNotification(UnitMovedNotification, unit);

        // If action was already selected, do action
        if (SelectionManager.TargetTile != null) {
            owner.ChangeState<PerformAbilityState>();
            yield break;
        }

        owner.ChangeState<AbilityTargetState>();
    }
}