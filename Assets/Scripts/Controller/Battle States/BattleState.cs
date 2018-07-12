using UnityEngine;
using System.Collections;

public abstract class BattleState : State
{
    public LevelData levelData;
    protected BattleController owner;
    public Board board { get { return owner.board; } }
    public CameraRig cameraRig { get { return owner.cameraRig; } }

    //public LevelData levelData { get { return owner.levelData; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }

    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }

    protected override void AddListeners() {
        this.AddObserver(OnMove, InputController.MoveNotification);
    }

    protected virtual void Awake() {
        owner = GetComponent<BattleController>();
    }

    protected virtual void OnMove(object sender, object args) {
    }

    protected override void RemoveListeners() {
        this.RemoveObserver(OnMove, InputController.MoveNotification);
    }

    protected virtual void SelectTile(Point p) {
        if (pos == p || !board.tiles.ContainsKey(p))
            return;

        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].center;
    }
}