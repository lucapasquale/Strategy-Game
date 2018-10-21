using UnityEngine;

public class Health : MonoBehaviour
{
    public const string UnitDiedNotification = "Health.UnitDiedNotification";


    public int MinHP = 0;
    private Stats stats;

    public int HP {
        get { return stats[StatTypes.HP]; }
        set { stats[StatTypes.HP] = value; }
    }

    public int MHP {
        get { return stats[StatTypes.MHP]; }
        set { stats[StatTypes.MHP] = value; }
    }


    private void Awake() {
        stats = GetComponent<Stats>();
    }

    private void OnEnable() {
        this.AddObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
        this.AddObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }

    private void OnDisable() {
        this.RemoveObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.RemoveObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }

    private void OnHPWillChange(object sender, object args) {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, MinHP, stats[StatTypes.MHP]));
    }

    private void OnHPDidChange(object sender, object args) {
        var unit = stats.GetComponent<Unit>();

        if (HP == 0) {
            unit.Disable();
            Debug.LogWarning($"Unit {unit} died");
            this.PostNotification(UnitDiedNotification, unit);
        }
    }

    private void OnMHPDidChange(object sender, object args) {
        int oldMHP = (int)args;
        if (MHP > oldMHP)
            HP += MHP - oldMHP;
        else
            HP = Mathf.Clamp(HP, MinHP, MHP);
    }
}