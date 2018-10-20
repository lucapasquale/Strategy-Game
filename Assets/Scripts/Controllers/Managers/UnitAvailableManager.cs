using UnityEngine;
using System.Collections.Generic;

public class UnitAvailableManager : MonoBehaviour
{
    public GameObject highlightTilePrefab;

    private List<GameObject> highlightInUse = new List<GameObject>();
    private BattleController owner;


    private void Awake() {
        owner = GetComponentInParent<BattleController>();
    }

    private void OnEnable() {
        this.AddObserver(ShowOnlySelected, RoundController.SelectedNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(ShowOnlySelected, RoundController.SelectedNotification);
    }

    private void ShowOnlySelected(object sender, object args) {
        Unit unit = args as Unit;
        Clear();

        if (unit != null) {
            Highlight(unit);
            return;
        }

        HighlightAvailable();
    }


    private void Clear() {
        foreach (var highlight in highlightInUse) {
            Destroy(highlight);
        }

        highlightInUse = new List<GameObject>();
    }

    private void Highlight(Unit unit) {
        var instance = Instantiate(highlightTilePrefab, unit.transform);
        highlightInUse.Add(instance);
    }

    private void HighlightAvailable() {
        Alliances currentSide = owner.roundController.actingSide;
        var roundUnits = owner.partyManager.GetUnits(currentSide);

        foreach (var unit in roundUnits) {
            if (unit.turn.IsAvailable()) {
                Highlight(unit);
            }
        }
    }
}