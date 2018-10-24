using UnityEngine;

public class Turn : MonoBehaviour
{
    public const string TargetSelectedNotification = "SelectionManager.TargetSelectedNotification";


    public Tile StartTile { get; private set; }
    public Tile MovementTile { get; private set; }
    public Tile TargetTile { get; private set; }

    private bool acted;
    private Unit actor;


    public void StartTurn() {
        StartTile = actor.Tile;
        acted = false;

        MovementTile = null;
        TargetTile = null;
    }

    public void SelectMovement(Tile tile) {
        MovementTile = tile;
    }

    public void SelectTarget(Tile tile) {
        TargetTile = tile;
        this.PostNotification(TargetSelectedNotification, tile);
    }

    public void EndTurn() {
        acted = true;
    }

    public bool IsAvailable() {
        return !acted;
    }


    private void Awake() {
        actor = GetComponentInParent<Unit>();
    }
}