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

    protected virtual void Awake() {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners() {
        this.AddObserver(OnMove, InputController.MoveNotification);
        this.AddObserver(OnFire, InputController.FireNotification);
    }

    protected override void RemoveListeners() {
        this.RemoveObserver(OnMove, InputController.MoveNotification);
        this.RemoveObserver(OnFire, InputController.FireNotification);
    }

    protected virtual void OnMove(object sender, object args) {
    }

    protected virtual void OnFire(object sender, object args) {
    }

    protected virtual void SelectTile(Point p) {
        if (pos == p || !board.tiles.ContainsKey(p))
            return;

        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].center;
    }
}