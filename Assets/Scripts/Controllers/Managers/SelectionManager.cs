using UnityEngine;
using System.Collections;


public class SelectionManager : Controller
{
    public const string TargetSelectedNotification = "SelectionManager.TargetSelectedNotification";


    public Tile MovementTile { get; private set; }
    public Tile TargetTile { get; private set; }


    public void SelectMovement(Tile tile) {
        MovementTile = tile;
    }

    public void SelectTarget(Tile tile) {
        TargetTile = tile;
        this.PostNotification(TargetSelectedNotification, tile);
    }


    private void OnEnable() {
        this.AddObserver(UnitSelected, RoundController.SelectedNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(UnitSelected, RoundController.SelectedNotification);
    }

    private void UnitSelected(object sender, object args) {
        MovementTile = null;
        TargetTile = null;
    }
}
