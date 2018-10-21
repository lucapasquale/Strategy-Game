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
        if (unit == null) {
            return;
        }

        AbilityTarget aTarget = unit.GetComponentInChildren<AbilityTarget>();
        foreach (Tile abilityTile in owner.rangeManager.AbilityRangeAndOrigin.Keys) {
            float intensity = aTarget.IsTarget(abilityTile) ? highIntensity : lowIntensity;
            HighlightTile(abilityTile, SetColorIntensity(attackColor, intensity));
        }

        HighlightTiles(owner.rangeManager.MoveRange, SetColorIntensity(moveColor, highIntensity));
        HighlightTile(unit.Tile, SetColorIntensity(moveColor, lowIntensity));
    }

    private void ClearAll() {
        foreach (var tile in usedHighlights) {
            tile.Paint(Color.clear);
        }

        usedHighlights.Clear();
    }


    private void HighlightTile(Tile tile, Color color) {
        tile.Paint(color);
        usedHighlights.Add(tile);
    }

    private void HighlightTiles(List<Tile> tiles, Color color) {
        foreach (Tile tile in tiles) {
            HighlightTile(tile, color);
        }
    }

    private Color SetColorIntensity(Color color, float intensity) {
        color.a = intensity;
        return color;
    }
}