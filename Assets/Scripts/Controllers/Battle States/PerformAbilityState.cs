using System.Collections;

public class PerformAbilityState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Animate());
    }

    private IEnumerator Animate() {
        // TODO play animations, etc
        yield return null;

        ApplyAbility();

        RoundController.EndTurn();
        owner.ChangeState<SelectUnitState>();
    }

    private void ApplyAbility() {
        Unit actor = RoundController.Current;

        var ability = actor.GetComponentInChildren<Ability>();
        ability.Perform(actor.turn.TargetTile);
    }
}