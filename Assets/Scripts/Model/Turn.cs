using UnityEngine;

public class Turn
{
    public bool hasUnitMoved;
    public GameObject ability;

    private readonly Tile startTile;
    private Unit actor;

    public Turn(Unit current) {
        actor = current;
        hasUnitMoved = false;
        startTile = actor.Tile;
    }

    public bool IsAvailable() {
        return !hasUnitMoved;
    }

    public void UndoMove() {
        hasUnitMoved = false;
        actor.Place(startTile);
        actor.Match();
    }
}