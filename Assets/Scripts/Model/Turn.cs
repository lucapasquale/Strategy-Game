using UnityEngine;

public class Turn : MonoBehaviour
{
    public bool hasUnitMoved;
    public Tile startTile;

    private Unit actor;


    private void Awake() {
        actor = GetComponentInParent<Unit>();
    }

    public void Start() {
        hasUnitMoved = false;
        startTile = actor.Tile;
    }

    public bool IsAvailable() {
        return !hasUnitMoved;
    }

    public void End() {
        hasUnitMoved = true;
    }

    public void UndoMove() {
        hasUnitMoved = false;
        actor.Place(startTile);
    }
}