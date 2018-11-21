using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public int duration;
    public int intensity;

    protected Unit _target { get; private set; }


    private void Awake() {
        _target = GetComponentInParent<Unit>();
    }

    public abstract void Apply();

    protected virtual void Remove() {
    }
}
