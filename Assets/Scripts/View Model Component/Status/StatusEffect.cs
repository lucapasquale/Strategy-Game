using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    protected int intensity;
    protected Unit _target { get; private set; }

    private int duration;


    private void Awake() {
        _target = GetComponentInParent<Unit>();
    }

    public virtual void Apply(int intensity, int duration) {
        this.intensity = intensity;
        this.duration = duration;

        this.AddObserver(ApplyEffect, RoundController.RoundStartedNotification);
    }

    public  virtual void Remove() {
        this.RemoveObserver(ApplyEffect, RoundController.RoundStartedNotification);
        Destroy(this);
    }

    protected virtual void ApplyEffect(object sender, object args) {
        Alliances? side = args as Alliances?;
        if (_target.alliance != side) {
            return;
        }

        duration--;
        if (duration < 0) {
            Remove();
        }
    }
}
