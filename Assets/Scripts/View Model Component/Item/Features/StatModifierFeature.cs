public class StatModifierFeature : Feature
{
    public StatTypes type;
    public int amount;

    private Stats stats {
        get {
            return _owner.GetComponentInParent<Stats>();
        }
    }

    protected override void OnApply() {
        stats[type] += amount;
    }

    protected override void OnRemove() {
        stats[type] -= amount;
    }
}