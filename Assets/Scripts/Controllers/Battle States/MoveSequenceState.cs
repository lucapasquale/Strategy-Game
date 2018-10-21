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
        yield return StartCoroutine(mov.Traverse(SelectionManager.MovementTile));

        // If action was already selected, do action
        if (SelectionManager.TargetTile) {
            owner.ChangeState<PerformAbilityState>();
            yield break;
        }

        owner.ChangeState<SelectActionState>();
    }
}