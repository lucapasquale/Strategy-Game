using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public const string SelectedNotification = "RoundController.SelectedNotification";


    public Unit Current { get; private set; }
    public Alliances actingSide = Alliances.Ally;

    public Dictionary<Alliances, List<Unit>> units = new Dictionary<Alliances, List<Unit>>() {
        { Alliances.Ally, new List<Unit>() },
        { Alliances.Enemy, new List<Unit>() },
    };


    public void Select(Unit unit) {
        if (unit != null && !units[unit.alliance].Exists(u => u == unit)) {
            throw new System.Exception("nao esta no round controller units");
        }

        Current = unit;
        this.PostNotification(SelectedNotification, Current);
    }

    public void EndTurn() {
        Current.Paint(new Color(0.5f, 0.5f, 0.5f));
        Select(null);

        if (units[actingSide].TrueForAll(u => !u.turn.IsAvailable())) {
            ChangeSides();
        }
    }


    private void OnEnable() {
        this.AddObserver(UnitSpawned, InitBattleState.UnitSpawnedNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(UnitSpawned, InitBattleState.UnitSpawnedNotification);
    }

    private void UnitSpawned(object sender, object args) {
        Unit unit = args as Unit;
        Alliances alliance = unit.alliance;

        if (alliance == Alliances.None) {
            return;
        }

        unit.turn = new Turn(unit);
        units[alliance].Add(unit);
    }

    private void ChangeSides() {
        foreach (var unit in units[actingSide]) {
            unit.Paint(Color.white);
        }

        actingSide = actingSide.GetOpposing();
        print($"Changing to side {actingSide}");

        foreach (var unit in units[actingSide]) {
            unit.turn = new Turn(unit);
        }
    }
}