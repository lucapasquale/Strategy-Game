using UnityEngine;
using System.Collections.Generic;

public class UnitAvailableManager : Controller
{
    public GameObject highlightTilePrefab;

    private List<GameObject> highlightInUse = new List<GameObject>();


    private void OnEnable() {
        this.AddObserver(UnitSelected, RoundController.SelectedNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(UnitSelected, RoundController.SelectedNotification);
    }

    private void UnitSelected(object sender, object args) {
        Unit unit = args as Unit;
        Clear();

        if (unit != null) {
            Highlight(unit);
            return;
        }

        var availableUnits = owner.partyController.GetAvailableUnits();
        foreach (var availableUnit in availableUnits) {
            Highlight(availableUnit);
        }
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
}