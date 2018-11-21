using UnityEngine;

public abstract class Feature : MonoBehaviour
{
    protected GameObject _owner { get; private set; }

    public void Activate(GameObject target) {
        if (_owner == null) {
            _owner = target;
            OnApply();
        }
    }

    public void Deactivate() {
        if (_owner != null) {
            OnRemove();
            _owner = null;
        }
    }

    public void Apply(GameObject target) {
        _owner = target;
        OnApply();
        _owner = null;
    }

    protected abstract void OnApply();

    protected virtual void OnRemove() {
    }
}