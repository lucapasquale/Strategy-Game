using UnityEngine;

public abstract class BattleState : State
{
    //public LevelData levelData { get { return owner.levelData; } }
    public LevelData levelData;

    protected BattleController owner;

    public Board Board { get { return owner.board; } }
    public CameraRig CameraRig { get { return owner.cameraRig; } }

    public RoundController RoundController { get { return owner.roundController; } }
    public RangeController RangeController { get { return owner.rangeController; } }
    public SelectionController SelectionController { get { return owner.selectionController; } }

    protected virtual void Awake() {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners() {
        InputController.TouchEvent += OnTouch;
    }

    protected override void RemoveListeners() {
        InputController.TouchEvent -= OnTouch;
    }

    protected virtual void OnTouch(object sender, InfoEventArgs<Point> e) {
    }
}