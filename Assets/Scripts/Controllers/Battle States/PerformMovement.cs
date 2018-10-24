using System.Collections;

public class PerformMovement : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence() {
        Unit actor = RoundController.Current;

        Movement mov = actor.GetComponentInChildren<Movement>();
        yield return StartCoroutine(mov.Traverse(actor.turn.MovementTile));

        // If action was already selected, do action
        if (actor.turn.TargetTile) {
            owner.ChangeState<ConfirmTargetState>();
            yield break;
        }

        owner.ChangeState<SelectActionState>();
    }
}