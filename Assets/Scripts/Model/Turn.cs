using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turn
{
    public bool hasUnitMoved;
    public GameObject ability;

    private Unit actor;
    private Tile startTile;
    private Directions startDir;

    public Turn(Unit current) {
        actor = current;
        hasUnitMoved = false;
        startTile = actor.tile;
        startDir = actor.dir;
    }

    public bool IsAvailable() {
        return !hasUnitMoved;
    }

    public void UndoMove() {
        hasUnitMoved = false;
        actor.Place(startTile);
        actor.dir = startDir;
        actor.Match();
    }
}