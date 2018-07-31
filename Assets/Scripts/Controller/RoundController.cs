using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public Alliances actingSide = Alliances.Ally;

    public Dictionary<Alliances, List<Unit>> units = new Dictionary<Alliances, List<Unit>>() {
        { Alliances.Ally, new List<Unit>() },
        { Alliances.Enemy, new List<Unit>() },
    };

    public Unit Current { get; private set; }

    public void AddUnit(Unit unit) {
        var alliance = unit.alliance;
        if (alliance == Alliances.None) {
            return;
        }

        unit.turn = new Turn(unit);
        units[alliance].Add(unit);
    }

    public void Select(Unit unit) {
        if (unit != null && !units[unit.alliance].Exists(u => u == unit)) {
            throw new System.Exception("nao esta no round controller units");
        }

        Current = unit;
    }

    public void EndTurn() {
        Current.GetComponent<SpriteRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
        Current = null;

        if (units[actingSide].TrueForAll(u => !u.turn.IsAvailable())) {
            ChangeSides();
        }
    }

    private void ChangeSides() {
        foreach (var unit in units[actingSide]) {
            unit.GetComponent<SpriteRenderer>().material.color = Color.white;
        }

        actingSide = actingSide.GetOpposing();
        print($"Changing to side {actingSide}");

        foreach (var unit in units[actingSide]) {
            unit.turn = new Turn(unit);
        }
    }
}