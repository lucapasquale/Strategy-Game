using System.Collections;

public class RestartUnitState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence() {
        Unit actor = RoundController.Current;

        Movement mov = actor.GetComponentInChildren<Movement>();
        yield return StartCoroutine(mov.Traverse(actor.turn.StartTile, 0.15f));

        owner.ChangeState<SelectUnitState>();
    }
}