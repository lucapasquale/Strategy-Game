using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState : State
{
    public LevelData levelData;
    protected BattleController owner;

    //public LevelData levelData { get { return owner.levelData; } }
    public Board Board { get { return owner.board; } }

    public CameraRig CameraRig { get { return owner.cameraRig; } }
    //public Point Pos { get { return owner.pos; } set { owner.pos = value; } }

    public RoundController RoundController { get { return owner.roundController; } }
    public SelectionController SelectionController { get { return owner.selectionController; } }
    public Transform TileSelectionIndicator { get { return owner.tileSelectionIndicator; } }

    protected virtual void Awake() {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners() {
        InputController.touchEvent += OnTouch;
    }

    protected override void RemoveListeners() {
        InputController.touchEvent -= OnTouch;
    }

    protected virtual void OnTouch(object sender, InfoEventArgs<Point> e) {
    }

    //protected virtual void SelectTile(Point p) {
    //    if (Pos == p || !Board.tiles.ContainsKey(p))
    //        return;

    //    Pos = p;
    //    TileSelectionIndicator.localPosition = Board.tiles[p].Center;
    //}

    protected virtual void ClearSelection() {
        TileSelectionIndicator.localPosition = new Vector3(-10, -10, 0);
    }
}