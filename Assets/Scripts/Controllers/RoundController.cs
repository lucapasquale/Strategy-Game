using System.Collections.Generic;
using UnityEngine;

public class RoundController : Controller
{
    public const string SelectedNotification = "RoundController.SelectedNotification";
    public const string TurnEndedNotification = "RoundController.TurnEndedNotification";
    public const string RoundStartedNotification = "RoundController.RoundStartedNotification";


    public Unit Current { get; private set; }
    public Alliances RoundSide { get; private set; } = Alliances.Ally;

    private List<Unit> currentUnits = new List<Unit>();


    public void StartGame() {
        StartRound(Alliances.Ally);
    }

    public void Select(Unit unit) {
        Current = unit;
        this.PostNotification(SelectedNotification, Current);
    }

    public void EndTurn() {
        Unit lastUnit = Current;
        lastUnit.Paint(new Color(0.5f, 0.5f, 0.5f));
        lastUnit.turn.EndTurn();

        Select(null);
        this.PostNotification(TurnEndedNotification, lastUnit);

        if (owner.partyController.GetAvailableUnits().Count == 0) {
            StartRound(RoundSide.GetOpposing());
        }
    }


    private void StartRound(Alliances side) {
        foreach (var unit in currentUnits) {
            unit.Paint(Color.white);
        }

        RoundSide = side;
        this.PostNotification(RoundStartedNotification, side);
        print($"Starting round for side {side}");

        currentUnits = owner.partyController.GetUnits(RoundSide).FindAll(u => u.isAlive);
        foreach (var unit in currentUnits) {
            unit.turn.StartTurn();
        }
    }
}