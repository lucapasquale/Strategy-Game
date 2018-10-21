using UnityEngine;
using System.Collections.Generic;

public class AreaHighlightManager : Controller
{
    public Color moveColor;
    public Color attackColor;

    private const float lowIntensity = 0.25f;
    private const float highIntensity = 0.5f;
    private List<Tile> usedHighlights = new List<Tile>();


    private void OnEnable() {
        this.AddObserver(UnitSelected, RoundController.SelectedNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(UnitSelected, RoundController.SelectedNotification);
    }

    private void UnitSelected(object sender, object args) {
        ClearAll();

        Unit unit = args as Unit;
        if (unit) {
            HighlightAttackArea(unit);
            HighlightMoveArea(unit);
        }
    }


    private void ClearAll() {
        foreach (var tile in usedHighlights) {
            tile.Paint(Color.clear);
        }

        usedHighlights.Clear();
    }

    private void HighlightAttackArea(Unit unit) {
        AbilityTarget aTarget = unit.GetComponentInChildren<AbilityTarget>();

        foreach (Tile abilityTile in owner.rangeManager.AbilityRangeAndOrigin.Keys) {
            float intensity = aTarget.IsTarget(abilityTile) ? highIntensity : lowIntensity;

            HighlightTile(abilityTile, SetColorIntensity(attackColor, intensity));
        }
    }

    private void HighlightMoveArea(Unit unit) {
        foreach (Tile tile in owner.rangeManager.MoveRange) {
            HighlightTile(tile, SetColorIntensity(moveColor, highIntensity));
        }

        HighlightTile(unit.Tile, SetColorIntensity(moveColor, lowIntensity));
    }


    private Color SetColorIntensity(Color color, float intensity) {
        color.a = intensity;
        return color;
    }

    private void HighlightTile(Tile tile, Color color) {
        tile.Paint(color);
        usedHighlights.Add(tile);
    }
}