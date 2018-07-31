using System.Collections;

public class MoveSequenceState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence() {
        Unit unit = RoundController.Current;

        Movement m = unit.GetComponent<Movement>();
        m.GetTilesInRange(Board);

        yield return StartCoroutine(m.Traverse(RangeController.moveTile));
        unit.turn.hasUnitMoved = true;

        if (RangeController.actTile != null) {
            owner.ChangeState<PerformAbilityState>();
            yield break;
        }

        owner.ChangeState<AbilityTargetState>();
    }
}