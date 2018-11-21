using UnityEngine;

public class PoisionStatusEffect : StatusEffect
{
    protected override void ApplyEffect(object sender, object args) {
        base.ApplyEffect(sender, args);

        Stats stats = _target.GetComponentInChildren<Stats>();
        stats[StatTypes.HP] -= intensity;
    }
}
