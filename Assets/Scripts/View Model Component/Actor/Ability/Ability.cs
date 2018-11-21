using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public const string CanPerformCheck = "Ability.CanPerformCheck";
    public const string FailedNotification = "Ability.FailedPerformNotification";
    public const string DidPerformNotification = "Ability.DidPerformNotification";

    public void Perform(Tile target) {
        if (!CanPerform()) {
            this.PostNotification(FailedNotification);
            return;
        }

        ApplyEffects(target);
        this.PostNotification(DidPerformNotification, target);
    }

    public bool CanPerform() {
        BaseException exc = new BaseException(true);
        this.PostNotification(CanPerformCheck, exc);
        return exc.toggle;
    }

    private void ApplyEffects(Tile target) {
        var effects = GetComponentsInChildren<AbilityEffect>();

        for (int i = 0; i < effects.Length; i++) {
            effects[i].Apply(target);
        }
    }
}