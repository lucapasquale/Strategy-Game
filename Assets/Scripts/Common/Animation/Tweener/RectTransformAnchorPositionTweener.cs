using UnityEngine;

public class RectTransformAnchorPositionTweener : Vector3Tweener
{
    private RectTransform rt;

    protected void Awake() {
        rt = transform as RectTransform;
    }

    protected override void OnUpdate() {
        base.OnUpdate();
        rt.anchoredPosition = currentTweenValue;
    }
}