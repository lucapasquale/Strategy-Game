using System.Collections;

public class MoveSequenceState : BattleState
{
    public override void Enter() {
        base.Enter();

        Unit unit = RoundController.Current;
        if (SelectionManager.MovementTile == unit.turn.startTile) {
            StartCoroutine(MoveToStart(unit));
            return;
        }

        StartCoroutine(MoveToAction(unit));
    }

    private IEnumerator MoveToAction(Unit unit) {
        Movement mov = unit.GetComponent<Movement>();
        mov.GetTilesInRange(Board);

        Tile destination = SelectionManager.MovementTile;
        yield return StartCoroutine(mov.Traverse(destination));

        // If action was already selected, do action
        if (SelectionManager.TargetTile) {
            owner.ChangeState<PerformAbilityState>();
            yield break;
        }

        owner.ChangeState<SelectActionState>();
    }

    private IEnumerator MoveToStart(Unit unit) {
        Movement mov = unit.GetComponent<Movement>();
        mov.GetTilesInRange(Board);

        Tile destination = unit.turn.startTile;
        yield return StartCoroutine(mov.Traverse(destination));

        owner.ChangeState<SelectUnitState>();
    }
}