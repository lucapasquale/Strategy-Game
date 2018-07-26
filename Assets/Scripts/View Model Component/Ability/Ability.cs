﻿using System.Collections.Generic;
using UnityEngine;

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
        var Effects = GetComponents<AbilityEffect>();
        for (int i = 0; i < Effects.Length; i++) {
            Effects[i].Apply(target);
        }
    }
}