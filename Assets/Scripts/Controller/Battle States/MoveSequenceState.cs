using UnityEngine;
using System.Collections;

public class MoveSequenceState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine("Sequence");
    }

    private IEnumerator Sequence() {
        Unit unit = roundController.current;
        Movement m = unit.GetComponent<Movement>();

        yield return StartCoroutine(m.Traverse(owner.currentTile));

        unit.turn.hasUnitMoved = true;
        roundController.End();

        owner.ChangeState<SelectUnitState>();
    }
}