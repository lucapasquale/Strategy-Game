using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundController : MonoBehaviour
{
    public Alliances actingSide = Alliances.Ally;
    public Unit current;

    public Dictionary<Alliances, List<Unit>> units = new Dictionary<Alliances, List<Unit>>() {
        { Alliances.Ally, new List<Unit>() },
        { Alliances.Enemy, new List<Unit>() },
    };

    public void AddUnit(Unit unit) {
        var alliance = unit.alliance;
        if (alliance == Alliances.None) {
            return;
        }

        units[alliance].Add(unit);
    }

    public void Select(Unit unit) {
        if (!units[unit.alliance].Exists(u => u == unit)) {
            throw new System.Exception("nao esta no round controller units");
        }

        current = unit;
    }

    public void End() {
        current.turn.isAvailable = false;
        current = null;

        if (units[actingSide].TrueForAll(u => !u.turn.isAvailable)) {
            actingSide = actingSide == Alliances.Ally ? Alliances.Enemy : Alliances.Ally;
        }
    }
}