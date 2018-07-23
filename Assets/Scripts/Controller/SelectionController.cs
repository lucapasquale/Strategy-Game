using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionController : MonoBehaviour
{
    public Tile moveTile;
    public Tile actTile;

    private Board board;
    public List<Tile> MovableTiles { get; private set; }
    public Dictionary<Tile, List<Tile>> ActionableTiles { get; private set; }

    public void SetMovable(List<Tile> _movalble) {
        MovableTiles = _movalble;
        Match();
    }

    public void SetActionable(Dictionary<Tile, List<Tile>> _actionable) {
        ActionableTiles = _actionable;
        Match();
    }

    public void SelectMove(Tile tile) {
        if (!MovableTiles.Contains(tile)) {
            Debug.LogError("Move tile not found");
            return;
        }

        moveTile = tile;
    }

    public void SelectAct(Tile tile) {
        if (!ActionableTiles.ContainsKey(tile) || ActionableTiles[tile].Count == 0) {
            Debug.LogError("Act tile not found");
            return;
        }

        actTile = ActionableTiles[tile][0];
    }

    private void Awake() {
        board = transform.parent.GetComponentInChildren<Board>();

        MovableTiles = new List<Tile>();
        ActionableTiles = new Dictionary<Tile, List<Tile>>();
    }

    private void OnEnable() {
        this.AddObserver(OnChangingSides, RoundController.ChangingSidesNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(OnChangingSides, RoundController.ChangingSidesNotification);
    }

    private void OnChangingSides(object sender, object args) {
        MovableTiles = new List<Tile>();
        ActionableTiles = new Dictionary<Tile, List<Tile>>();

        board.ClearSelection();
    }

    private void Match() {
        var emptyActionTiles = new List<Tile>();
        var filledActionTiles = new List<Tile>();

        foreach (var actionTile in ActionableTiles) {
            if (actionTile.Key == null) {
                emptyActionTiles.Add(actionTile.Key);
                continue;
            }

            filledActionTiles.Add(actionTile.Key);
        }

        board.SelectTiles(emptyActionTiles, Color.magenta);
        board.SelectTiles(filledActionTiles, Color.red);
        board.SelectTiles(MovableTiles, Color.blue);
    }
}