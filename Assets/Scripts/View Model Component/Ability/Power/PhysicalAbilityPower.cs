using UnityEngine;
using System.Collections;

public class PhysicalAbilityPower : AbilityPower
{
    public int level;

    protected override int GetBaseAttack() {
        return GetComponentInParent<Stats>()[StatTypes.ATK];
    }

    protected override int GetBaseDefense(Unit target) {
        return target.GetComponent<Stats>()[StatTypes.DEF];
    }

    protected override int GetPower() {
        return level;
    }
}