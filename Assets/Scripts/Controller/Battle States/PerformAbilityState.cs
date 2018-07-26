using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject target = SelectionController.actTile.content;
        Health health = target.GetComponent<Health>();

        health.HP -= 10;
    }
}