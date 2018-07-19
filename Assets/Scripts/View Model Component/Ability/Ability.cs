using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ability : MonoBehaviour
{
    public const string CanPerformCheck = "Ability.CanPerformCheck";
    public const string FailedNotification = "Ability.FailedNotification";
    public const string DidPerformNotification = "Ability.DidPerformNotification";

    public void Perform(List<Tile> targets) {
        if (!CanPerform()) {
            this.PostNotification(FailedNotification);
            return;
        }

        for (int i = 0; i < targets.Count; ++i)
            Perform(targets[i]);

        this.PostNotification(DidPerformNotification);
    }

    public bool CanPerform() {
        BaseException exc = new BaseException(true);
        this.PostNotification(CanPerformCheck, exc);
        return exc.toggle;
    }

    private void Perform(Tile target) {
        for (int i = 0; i < transform.childCount; ++i) {
            Transform child = transform.GetChild(i);
            AbilityEffect effect = child.GetComponent<AbilityEffect>();
            effect.Apply(target);
        }
    }
}