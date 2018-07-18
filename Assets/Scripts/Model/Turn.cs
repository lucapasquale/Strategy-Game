using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turn
{
    public Unit actor;
    public bool isAvailable;
    public bool hasUnitMoved;
    public bool hasUnitActed;
    public GameObject ability;
    private Tile startTile;
    private Directions startDir;

    public Turn(Unit current) {
        actor = current;
        isAvailable = true;
        hasUnitMoved = false;
        hasUnitActed = false;
        startTile = actor.tile;
        startDir = actor.dir;
    }

    public void UndoMove() {
        hasUnitMoved = false;
        actor.Place(startTile);
        actor.dir = startDir;
        actor.Match();
    }
}