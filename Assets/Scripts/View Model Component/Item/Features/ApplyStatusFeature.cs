using UnityEngine;
using System.Collections;

public enum StatusEffects {
    Poison
}

public class ApplyStatusFeature : Feature
{
    public int duration;
    public int intensity;
    public StatusEffects effect;

    private Ability ability;


    protected override void OnApply() {
        Unit unit = _owner.GetComponentInParent<Unit>();
        ability = unit.GetComponentInChildren<Ability>();

        this.AddObserver(ApplyStatus, Ability.DidPerformNotification, ability);
    }

    protected override void OnRemove() {
        this.RemoveObserver(ApplyStatus, Ability.DidPerformNotification, ability);
    }


    private void ApplyStatus(object sender, object args) {
        Tile tile = args as Tile;
        if (!tile || !tile.content) {
            return;
        }

        Unit target = tile.content.GetComponent<Unit>();

        StatusEffect appliedEffect = null;
        switch (effect) {
            case StatusEffects.Poison:
                appliedEffect = target.gameObject.AddComponent<PoisionStatusEffect>();
                break;
        }

        appliedEffect.duration = duration;
        appliedEffect.intensity = intensity;
        appliedEffect.Apply();
    }
}