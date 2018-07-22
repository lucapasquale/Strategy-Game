using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionController : MonoBehaviour
{
    public Tile moveTile;
    public Tile actTile;

    private Board board;
    public List<Tile> MovableTiles { get; private set; }
    public List<Tile> ActionableTiles { get; private set; }

    public void SetMovable(List<Tile> _movalble) {
        MovableTiles = _movalble;
        Match();
    }

    public void SetActionable(List<Tile> _actionable) {
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
        if (!ActionableTiles.Contains(tile)) {
            Debug.LogError("Act tile not found");
            return;
        }

        actTile = tile;
    }

    private void Awake() {
        board = transform.parent.GetComponentInChildren<Board>();

        MovableTiles = new List<Tile>();
        ActionableTiles = new List<Tile>();
    }

    private void OnEnable() {
        this.AddObserver(OnChangingSides, RoundController.ChangingSidesNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(OnChangingSides, RoundController.ChangingSidesNotification);
    }

    private void OnChangingSides(object sender, object args) {
        MovableTiles = new List<Tile>();
        ActionableTiles = new List<Tile>();

        board.ClearSelection();
    }

    private void Match() {
        board.SelectTiles(ActionableTiles, Color.magenta);

        var possibleActions = new List<Tile>(ActionableTiles);
        possibleActions.RemoveAll(t => t.content == null);

        board.SelectTiles(MovableTiles, Color.blue);
        board.SelectTiles(possibleActions, Color.red);
    }
}