using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public const string SelectedNotification = "RoundController.SelectedNotification";
    public const string TurnEndedNotification = "RoundController.TurnEndedNotification";
    public const string RoundStartedNotification = "RoundController.RoundStartedNotification";


    public Unit Current { get; private set; }
    public Alliances actingSide = Alliances.Ally;

    private List<Unit> currentUnits = new List<Unit>();
    private BattleController owner;


    public void StartGame() {
        StartRound(Alliances.Ally);
    }

    public void Select(Unit unit) {
        Current = unit;
        this.PostNotification(SelectedNotification, Current);
    }

    public void EndTurn() {
        Current.Paint(new Color(0.5f, 0.5f, 0.5f));
        Current.turn.End();

        Unit lastUnit = Current;

        Select(null);
        this.PostNotification(TurnEndedNotification, lastUnit);

        if (GetAvailableUnits().Count == 0) {
            StartRound(actingSide.GetOpposing());
        }
    }


    private void Awake() {
        owner = GetComponentInParent<BattleController>();
    }

    public void StartRound(Alliances side) {
        foreach (var unit in currentUnits) {
            unit.Paint(Color.white);
        }

        actingSide = side;
        currentUnits = owner.partyManager.GetUnits(actingSide).FindAll(u => u.isAlive);

        print($"Starting round for side {side}");
        this.PostNotification(RoundStartedNotification, side);

        foreach (var unit in currentUnits) {
            unit.turn = new Turn(unit);
        }
    }

    private List<Unit> GetAvailableUnits() {
        return currentUnits.FindAll(u => u.turn.IsAvailable());
    }
}