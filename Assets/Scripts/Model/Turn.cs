using UnityEngine;

public class Turn
{
    public bool isAvailable;
    public GameObject ability;

    private Unit actor;
    private Tile startTile;
    private Directions startDir;

    public Turn(Unit current) {
        actor = current;
        isAvailable = true;
        startTile = actor.tile;
        startDir = actor.dir;
    }

    public void UndoMove() {
        isAvailable = true;
        actor.Place(startTile);
        actor.dir = startDir;
        actor.Match();
    }
}