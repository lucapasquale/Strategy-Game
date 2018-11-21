using System.Collections;

public class RestartUnitState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Sequence());


        Unit actor = RoundController.Current;
        actor.turn.SelectMovement(null);
        actor.turn.SelectTarget(null);
    }

    private IEnumerator Sequence() {
        Unit actor = RoundController.Current;

        Movement mov = actor.GetComponentInChildren<Movement>();
        yield return StartCoroutine(mov.Traverse(actor.turn.StartTile, 0.15f));

        owner.ChangeState<SelectUnitState>();
    }
}