using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public Tile moveTile;
    public Tile actTile;

    private Board board;
    public List<Tile> MoveTiles { get; private set; }
    public Dictionary<Tile, List<Tile>> ActOriginTiles { get; private set; }

    public void SelectMove(Tile tile) {
        if (!MoveTiles.Contains(tile)) {
            Debug.LogError("Move tile not found");
            return;
        }

        moveTile = tile;
    }

    public void SelectAct(Tile tile) {
        if (!ActOriginTiles.ContainsKey(tile) || ActOriginTiles[tile].Count == 0) {
            Debug.LogError("Act tile not found");
            return;
        }

        actTile = ActOriginTiles[tile][0];
    }

    public void SetMovable(List<Tile> movableTiles) {
        MoveTiles = movableTiles;
        Match();
    }

    public void SetActionable(Dictionary<Tile, List<Tile>> actionable) {
        ActOriginTiles = actionable;
        Match();
    }

    public void Clear() {
        MoveTiles = new List<Tile>();
        moveTile = null;

        ActOriginTiles = new Dictionary<Tile, List<Tile>>();
        actTile = null;

        board.ClearSelection();
    }

    private void Awake() {
        board = transform.parent.GetComponentInChildren<Board>();

        MoveTiles = new List<Tile>();
        ActOriginTiles = new Dictionary<Tile, List<Tile>>();
    }

    private void Match() {
        var emptyActionTiles = new List<Tile>();
        var filledActionTiles = new List<Tile>();

        foreach (var actionTile in ActOriginTiles) {
            if (actionTile.Key == null) {
                emptyActionTiles.Add(actionTile.Key);
                continue;
            }

            filledActionTiles.Add(actionTile.Key);
        }

        board.SelectTiles(emptyActionTiles, Color.magenta);
        board.SelectTiles(filledActionTiles, Color.red);
        board.SelectTiles(MoveTiles, Color.blue);
    }
}