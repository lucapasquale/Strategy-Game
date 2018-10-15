using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect : MonoBehaviour
{
    public const string GetAttackNotification = "BaseAbilityEffect.GetAttackNotification";
    public const string GetDefenseNotification = "BaseAbilityEffect.GetDefenseNotification";
    public const string TweakDamageNotification = "BaseAbilityEffect.TweakDamageNotification";
    public const string HitNotification = "BaseAbilityEffect.HitNotification";


    public abstract int Predict(Tile target);

    public void Apply(Tile target) {
        if (GetComponent<AbilityTarget>().IsTarget(target) == false)
            return;

        this.PostNotification(HitNotification, OnApply(target));
    }

    protected abstract int OnApply(Tile target);

    protected virtual int GetStat(Unit attacker, Unit target, string notification, int startValue) {
        var mods = new List<ValueModifier>();
        var info = new Info<Unit, Unit, List<ValueModifier>>(attacker, target, mods);
        this.PostNotification(notification, info);
        mods.Sort(Compare);

        float value = startValue;
        for (int i = 0; i < mods.Count; ++i)
            value = mods[i].Modify(startValue, value);

        return Mathf.FloorToInt(value);
    }

    private int Compare(ValueModifier x, ValueModifier y) {
        return x.sortOrder.CompareTo(y.sortOrder);
    }
}