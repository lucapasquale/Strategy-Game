﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbilityEffect : MonoBehaviour
{
    public const string GetAttackNotification = "BaseAbilityEffect.GetAttackNotification";
    public const string GetDefenseNotification = "BaseAbilityEffect.GetDefenseNotification";
    public const string GetPowerNotification = "BaseAbilityEffect.GetPowerNotification";
    public const string TweakDamageNotification = "BaseAbilityEffect.TweakDamageNotification";
    public const string HitNotification = "BaseAbilityEffect.HitNotification";
    protected const int minDamage = -999;
    protected const int maxDamage = 999;

    public abstract int Predict(Tile target);

    public void Apply(Tile target) {
        if (GetComponent<AbilityEffectTarget>().IsTarget(target) == false)
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

        int retValue = Mathf.FloorToInt(value);
        retValue = Mathf.Clamp(retValue, minDamage, maxDamage);
        return retValue;
    }

    private int Compare(ValueModifier x, ValueModifier y) {
        return x.sortOrder.CompareTo(y.sortOrder);
    }
}