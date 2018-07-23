using UnityEngine;

public abstract class BattleState : State
{
    //public LevelData levelData { get { return owner.levelData; } }
    public LevelData levelData;

    protected BattleController owner;

    public Board Board { get { return owner.board; } }
    public CameraRig CameraRig { get { return owner.cameraRig; } }

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
}