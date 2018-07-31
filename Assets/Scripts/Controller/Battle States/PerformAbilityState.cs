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
        var target = SelectionController.actTile;

        var effects = RoundController.Current.GetComponentsInChildren<AbilityEffect>();
        for (int i = 0; i < effects.Length; i++) {
            effects[i].Apply(target);
        }
    }
}