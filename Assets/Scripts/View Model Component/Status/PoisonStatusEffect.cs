using UnityEngine;

public class PoisionStatusEffect : StatusEffect
{
    public override void Apply() {
        this.AddObserver(ApplyDamage, RoundController.RoundStartedNotification);
    }

    protected override void Remove() {
        base.Remove();

        this.RemoveObserver(ApplyDamage, RoundController.RoundStartedNotification);
        Destroy(this);
    }

    private void ApplyDamage(object sender, object args) {
        Alliances? side = args as Alliances?;
        if (_target.alliance != side) {
            return;
        }

        Stats stats = _target.GetComponentInChildren<Stats>();
        stats[StatTypes.HP] -= intensity;

        duration--;
        if (duration == 0) {
            Remove();
        }
    }
}
