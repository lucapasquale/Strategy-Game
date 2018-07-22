using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionController : MonoBehaviour
{
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

    public void Clear() {
        MovableTiles = new List<Tile>();
        ActionableTiles = new List<Tile>();

        board.ClearSelection();
    }

    private void Awake() {
        board = transform.parent.GetComponentInChildren<Board>();
        Clear();
    }

    private void Match() {
        board.SelectTiles(MovableTiles, Color.blue);
        board.SelectTiles(ActionableTiles, Color.magenta);

        var possibleActions = new List<Tile>(ActionableTiles);
        possibleActions.RemoveAll(t => t.content == null);

        board.SelectTiles(possibleActions, Color.red);
    }
}