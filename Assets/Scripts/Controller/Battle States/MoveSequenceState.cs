using System.Collections;

public class MoveSequenceState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine("Sequence");
    }

    private IEnumerator Sequence() {
        Unit actor = RoundController.Current;

        Movement m = actor.GetComponent<Movement>();
        m.GetTilesInRange(Board);

        yield return StartCoroutine(m.Traverse(SelectionController.moveTile));
        actor.turn.hasUnitMoved = true;

        if (SelectionController.actTile != null) {
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            yield break;
        }

        owner.ChangeState<AbilityTargetState>();
    }
}