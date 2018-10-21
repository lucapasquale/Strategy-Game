using System.Collections;

public class RestartUnitState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence() {
        Unit unit = RoundController.Current;

        Movement mov = unit.GetComponent<Movement>();
        yield return StartCoroutine(mov.Traverse(unit.turn.startTile, 0.15f));

        owner.ChangeState<SelectUnitState>();
    }
}