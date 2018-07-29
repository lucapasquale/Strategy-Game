using UnityEngine;
using System.Collections.Generic;

public abstract class AbilityPower : MonoBehaviour
{
    protected abstract int GetBaseAttack();

    protected abstract int GetBaseDefense(Unit target);

    protected abstract int GetPower();

    private void OnEnable() {
        this.AddObserver(OnGetBaseAttack, AbilityEffect.GetAttackNotification);
        this.AddObserver(OnGetBaseDefense, AbilityEffect.GetDefenseNotification);
        this.AddObserver(OnGetPower, AbilityEffect.GetPowerNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(OnGetBaseAttack, AbilityEffect.GetAttackNotification);
        this.RemoveObserver(OnGetBaseDefense, AbilityEffect.GetDefenseNotification);
        this.RemoveObserver(OnGetPower, AbilityEffect.GetPowerNotification);
    }

    private void OnGetBaseAttack(object sender, object args) {
        if (IsMyEffect(sender)) {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add(new AddValueModifier(0, GetBaseAttack()));
        }
    }

    private void OnGetBaseDefense(object sender, object args) {
        if (IsMyEffect(sender)) {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add(new AddValueModifier(0, GetBaseDefense(info.arg1)));
        }
    }

    private void OnGetPower(object sender, object args) {
        if (IsMyEffect(sender)) {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add(new AddValueModifier(0, GetPower()));
        }
    }

    private bool IsMyEffect(object sender) {
        MonoBehaviour obj = sender as MonoBehaviour;
        return (obj != null && obj.transform.parent == transform);
    }
}