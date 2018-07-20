using System.Collections;

public class MoveSequenceState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine("Sequence");
    }

    private IEnumerator Sequence() {
        Unit actor = RoundController.current;
        Movement m = actor.GetComponent<Movement>();

        yield return StartCoroutine(m.Traverse(owner.currentTile));

        RoundController.EndTurn();
        owner.ChangeState<SelectUnitState>();
    }
}