using UnityEngine;
using System.Collections;

public class MoveSequenceState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine("Sequence");
    }

    private IEnumerator Sequence() {
        Movement m = roundController.current.GetComponent<Movement>();
        yield return StartCoroutine(m.Traverse(owner.currentTile));
        roundController.End();

        owner.ChangeState<SelectUnitState>();
    }
}