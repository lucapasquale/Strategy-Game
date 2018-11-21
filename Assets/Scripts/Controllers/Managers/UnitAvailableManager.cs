using UnityEngine;
using System.Collections.Generic;

public class UnitAvailableManager : Controller
{
    public GameObject highlightTilePrefab;

    public Color allyColor;
    public Color enemyColor;

    private List<GameObject> teamHighlights = new List<GameObject>();
    private GameObject targetHighlight;


    private void OnEnable() {
        this.AddObserver(UnitSelected, RoundController.SelectedNotification);
        this.AddObserver(TargetSelected, Turn.TargetSelectedNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(UnitSelected, RoundController.SelectedNotification);
        this.RemoveObserver(TargetSelected, Turn.TargetSelectedNotification);
    }

    private void UnitSelected(object sender, object args) {
        Clear();

        Unit unit = args as Unit;
        if (unit) {
            HighlightUnit(unit);
            return;
        }

        var availableUnits = owner.partyController.GetAvailableUnits();
        foreach (var availableUnit in availableUnits) {
            HighlightUnit(availableUnit);
        }
    }

    private void TargetSelected(object sender, object args) {
        Tile tile = args as Tile;
        if (!tile || !tile.content) {
            Destroy(targetHighlight);
            return;
        }

        var instance = Instantiate(highlightTilePrefab, tile.transform);
        instance.GetComponent<SpriteRenderer>().color = enemyColor;

        targetHighlight = instance;
    }


    private void Clear() {
        Destroy(targetHighlight);

        foreach (var highlight in teamHighlights) {
            Destroy(highlight);
        }

        targetHighlight = null;
        teamHighlights = new List<GameObject>();
    }

    private void HighlightUnit(Unit unit) {
        var instance = Instantiate(highlightTilePrefab, unit.transform);
        instance.GetComponent<SpriteRenderer>().color = allyColor;
        
        teamHighlights.Add(instance);
    }
}