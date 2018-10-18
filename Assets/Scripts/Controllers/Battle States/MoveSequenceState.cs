using System.Collections;

public class MoveSequenceState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence() {
        Unit unit = RoundController.Current;
        Movement mov = unit.GetComponent<Movement>();

        mov.GetTilesInRange(Board);
        yield return StartCoroutine(mov.Traverse(RangeController.moveTile));

        unit.turn.hasUnitMoved = true;

        // if action was already selected, do action
        if (RangeController.actTile != null) {
            owner.ChangeState<PerformAbilityState>();
            yield break;
        }

        owner.ChangeState<AbilityTargetState>();
    }
}