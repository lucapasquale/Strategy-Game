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
        var ability = RoundController.Current.GetComponentInChildren<Ability>();
        var target = RangeController.actTile;

        ability.Perform(target);
    }
}